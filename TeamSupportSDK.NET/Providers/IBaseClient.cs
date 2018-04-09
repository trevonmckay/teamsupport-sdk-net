using System.Net.Http;
using System.Threading.Tasks;

namespace TeamSupportSDK.NET.Providers
{
    public interface IBaseClient
    {
        /// <summary>
        /// Gets the <see cref="IAuthenticationProvider"/> for authenticating HTTP requests.
        /// </summary>
        IAuthenticationProvider AuthenticationProvider { get; }

        /// <summary>
        /// Gets the base URL for requests of the client.
        /// </summary>
        string BaseUrl { get; }

        /// <summary>
        /// Gets the <see cref="IHttpProvider"/> for sending HTTP requests.
        /// </summary>
        IHttpProvider HttpProvider { get; }
    }

    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Authenticates the specified request message.
        /// </summary>
        /// <param name="request">The HttpRequestMessage to authenticate</param>
        /// <returns>The task to await</returns>
        Task AuthenticateRequestAsync(HttpRequestMessage request);
    }

    public interface IHttpProvider
    {
        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage"/> to send.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
