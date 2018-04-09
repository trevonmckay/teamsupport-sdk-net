using System;
using System.Collections.Generic;
using System.Text;
using TeamSupport.NET.SDK.Providers;

namespace TeamSupport.NET.SDK.Requests
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
