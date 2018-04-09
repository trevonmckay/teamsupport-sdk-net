using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TeamSupportSDK.NET.Providers;

namespace TeamSupportSDK.NET.Requests
{
    public class BaseRequest
    {
        public IBaseClient Client { get; private set; }

        public string ContentType { get; set; }

        public string Method { get; set; }

        public string RequestUrl { get; set; }

        public BaseRequest(string requestUrl, IBaseClient client)
        {
            this.Method = Constants.Core.HTTPMethods.GET;
            this.Client = client;
            this.RequestUrl = requestUrl;
        }

        public async Task<T> SendAsync<T>(object serializableObject)
        {
            using (var response = await this.SendRequestAsync(serializableObject).ConfigureAwait(false))
            {
                if(response.Content != null)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseString);
                }

                return default(T);
            }
        }

        public async Task<HttpResponseMessage> SendRequestAsync(object serializableObject)
        {
            if(string.IsNullOrWhiteSpace(this.RequestUrl))
            {
                throw new ArgumentNullException("RequestUrl", "The property RequestUrl cannot be null or empty.");
            }

            var httpClient = this.Client.HttpProvider;
            using (var request = this.GetHttpRequestMessage())
            {
                await this.AuthenticateRequest(request).ConfigureAwait(false);

                if(serializableObject != null)
                {
                    var inputStream = serializableObject as Stream;

                    if (inputStream != null)
                    {
                        request.Content = new StreamContent(inputStream);
                    }
                    else
                    {
                        request.Content = new StringContent(JsonConvert.SerializeObject(serializableObject));
                    }

                    if (!string.IsNullOrEmpty(this.ContentType))
                    {
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue(this.ContentType);
                    }
                }

                return await httpClient.SendAsync(request).ConfigureAwait(false);
            }
        }

        public HttpRequestMessage GetHttpRequestMessage()
        {
            var request = new HttpRequestMessage(new HttpMethod(this.Method), this.RequestUrl);

            return request;
        }

        private Task AuthenticateRequest(HttpRequestMessage request)
        {
            return this.Client.AuthenticationProvider.AuthenticateRequestAsync(request);
        }
    }
}
