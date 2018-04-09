using TeamSupportSDK.NET.Providers;

namespace TeamSupportSDK.NET.Requests
{
    public class CustomerRequestBuilder : BaseRequestBuilder
    {
        public CustomerRequestBuilder(string requestUrl, IBaseClient client) : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The <see cref="CustomerRequest"/> request.</returns>
        public CustomerRequest Request()
        {
            return new CustomerRequest(this.RequestUrl, this.Client);
        }
    }
}
