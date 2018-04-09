using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TeamSupport.NET.SDK.Constants;
using TeamSupport.NET.SDK.Models;

namespace TeamSupport.NET.SDK.Providers
{
    public class DefaultHttpProvider : IHttpProvider
    {
        private const int maxRedirects = 5;

        private const int maxRetries = 3;

        internal bool disposeHandler;

        internal HttpClient httpClient;

        internal HttpMessageHandler httpMessageHandler;

        public DefaultHttpProvider() : this((HttpMessageHandler)null, true) { }

        public DefaultHttpProvider(HttpClientHandler httpClientHandler, bool disposeHandler) : this((HttpMessageHandler)httpClientHandler, disposeHandler) { }

        internal DefaultHttpProvider(HttpMessageHandler httpMessageHandler, bool disposeHandler)
        {
            this.disposeHandler = disposeHandler;
            this.httpMessageHandler = httpMessageHandler ?? new HttpClientHandler { AllowAutoRedirect = false };
            this.httpClient = new HttpClient(this.httpMessageHandler, this.disposeHandler);

            // this.CacheControlHeader = new CacheControlHeaderValue { NoCache = true, NoStore = true };
        }

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage"/> to send.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return this.SendAsync(request, HttpCompletionOption.ResponseContentRead);
        }

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage"/> to send.</param>
        /// <param name="completionOption">The <see cref="HttpCompletionOption"/> to pass to the <see cref="IHttpProvider"/> on send.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
        {
            var response = await this.SendRequestAsync(request, completionOption);

            if (this.IsRedirect(response.StatusCode))
            {
                response = await this.HandleRedirect(response, completionOption);

                if (response == null)
                {
                    throw new ServiceException(
                        new Error
                        {
                            Code = Errors.Codes.GeneralException,
                            Message = Errors.Messages.LocationHeaderNotSetOnRedirect,
                        });
                }
            }

            if (this.IsRateLimited(response))
            {
                response = await this.HandleRateLimitAsync(response, completionOption);
            }

            if (!response.IsSuccessStatusCode && !this.IsRedirect(response.StatusCode))
            {
                using (response)
                {
                    var errorResponse = await this.ConvertErrorResponseAsync(response);
                    Error error = null;

                    if (errorResponse == null || errorResponse.Error == null)
                    {
                        if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                        {
                            error = new Error { Code = Errors.Codes.ItemNotFound };
                        }
                        else
                        {
                            error = new Error
                            {
                                Code = Errors.Codes.GeneralException,
                                Message = Errors.Messages.UnexpectedExceptionResponse,
                            };
                        }
                    }
                    else
                    {
                        error = errorResponse.Error;
                    }

                    if (string.IsNullOrEmpty(error.ThrowSite))
                    {
                        IEnumerable<string> throwsiteValues;

                        if (response.Headers.TryGetValues(Core.Headers.ThrowSiteHeaderName, out throwsiteValues))
                        {
                            error.ThrowSite = throwsiteValues.FirstOrDefault();
                        }
                    }

                    throw new ServiceException(error);
                }
            }

            return response;
        }

        private async Task<HttpResponseMessage> HandleRateLimitAsync(HttpResponseMessage response, HttpCompletionOption completionOption, int rateLimitCount = 0)
        {
            var defaultWait = (60 * rateLimitCount) + 60;
            var retryAfterSpan = response.Headers.RetryAfter.Delta ?? TimeSpan.FromSeconds(defaultWait);

            await Task.Delay(retryAfterSpan);
            response = await this.SendRequestAsync(response.RequestMessage, completionOption);

            if (this.IsRateLimited(response))
            {
                if (++rateLimitCount > DefaultHttpProvider.maxRetries)
                {
                    var retyAfterTime = response.Headers.RetryAfter.Date;
                    throw new ServiceException(new Error
                    {
                        Code = Errors.Codes.QuotaExceeded,
                        Message = string.Format(Errors.Messages.QuotaExceeded, retyAfterTime)
                    });
                }

                response = await this.HandleRateLimitAsync(response, completionOption, rateLimitCount);
            }

            return response;
        }

        internal async Task<HttpResponseMessage> HandleRedirect(HttpResponseMessage initialResponse, HttpCompletionOption completionOption, int redirectCount = 0)
        {
            if (initialResponse.Headers.Location == null)
            {
                return null;
            }

            using (initialResponse)
            using (var redirectRequest = new HttpRequestMessage(initialResponse.RequestMessage.Method, initialResponse.Headers.Location))
            {
                // Preserve headers for the next request
                foreach (var header in initialResponse.RequestMessage.Headers)
                {
                    redirectRequest.Headers.Add(header.Key, header.Value);
                }

                var response = await this.SendRequestAsync(redirectRequest, completionOption);

                if (this.IsRedirect(response.StatusCode))
                {
                    if (++redirectCount > DefaultHttpProvider.maxRedirects)
                    {
                        throw new ServiceException(
                            new Error
                            {
                                Code = Errors.Codes.TooManyRedirects,
                                Message = string.Format(Errors.Messages.TooManyRedirectsFormatString, DefaultHttpProvider.maxRedirects)
                            });
                    }

                    return await this.HandleRedirect(response, completionOption, redirectCount);
                }

                return response;
            }
        }

        internal async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
        {
            return await this.httpClient.SendAsync(request, completionOption);
        }

        /// <summary>
        /// Converts the <see cref="HttpRequestException"/> into an <see cref="ErrorResponse"/> object;
        /// </summary>
        /// <param name="response">The <see cref="WebResponse"/> to convert.</param>
        /// <returns>The <see cref="ErrorResponse"/> object.</returns>
        private async Task<ErrorResponse> ConvertErrorResponseAsync(HttpResponseMessage response)
        {
            try
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ErrorResponse>(responseString);
            }
            catch (Exception)
            {
                // If there's an exception deserializing the error response return null and throw a generic
                // ServiceException later.
                return null;
            }
        }

        private bool IsRedirect(HttpStatusCode statusCode)
        {
            return (int)statusCode >= 300 && (int)statusCode < 400 && statusCode != HttpStatusCode.NotModified;
        }

        private bool IsRateLimited(HttpResponseMessage response)
        {
            return (response.StatusCode == HttpStatusCode.Forbidden && response.ReasonPhrase.Equals("Quota Exceeded", StringComparison.CurrentCultureIgnoreCase));
        }

        public void Dispose()
        {
            if(this.httpClient != null)
            {
                this.httpClient.Dispose();
            }
        }
    }
}
