using TeamSupportSDK.NET.Providers;

namespace TeamSupportSDK.NET.Requests
{
    public class ContactsCollectionRequestBuilder : BaseRequestBuilder
    {
        public ContactsCollectionRequestBuilder(string requestUrl, IBaseClient client) : base(requestUrl, client) { }

        public ContactsCollectionRequest Request()
        {
            return new ContactsCollectionRequest(this.RequestUrl, this.Client);
        }

        public ContactRequestBuilder this[string id]
        {
            get
            {
                var requestUrl = this.AppendSegmentToRequestUrl("/" + id);
                return new ContactRequestBuilder(requestUrl, this.Client);
            }
        }
    }
}
