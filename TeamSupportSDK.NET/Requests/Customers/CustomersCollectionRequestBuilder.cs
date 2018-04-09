using TeamSupportSDK.NET.Providers;

namespace TeamSupportSDK.NET.Requests
{
    public class CustomersCollectionRequestBuilder : BaseRequestBuilder
    {
        public CustomersCollectionRequestBuilder(string requestUrl, IBaseClient client) : base(requestUrl, client)
        {
        }

        public CustomersCollectionRequest Request()
        {
            return new CustomersCollectionRequest(this.RequestUrl, this.Client);
        }

        public CustomerRequestBuilder this[string id]
        {
            get
            {
                var requestUrl = this.AppendSegmentToRequestUrl("/" + id);
                return new CustomerRequestBuilder(requestUrl, this.Client);
            }
        }
    }
}
