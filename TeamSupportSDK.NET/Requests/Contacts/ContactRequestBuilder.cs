using TeamSupportSDK.NET.Providers;

namespace TeamSupportSDK.NET.Requests
{
    public class ContactRequestBuilder : BaseRequestBuilder
    {
        public ContactRequestBuilder(string requestUrl, IBaseClient client) : base(requestUrl, client) { }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The <see cref="ContactRequest"/> request.</returns>
        public ContactRequest Request()
        {
            return new ContactRequest(this.RequestUrl, this.Client);
        }
    }
}
