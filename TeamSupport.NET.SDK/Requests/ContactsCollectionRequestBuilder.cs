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
                return new ContactRequestBuilder(this.RequestUrl, this.Client);
            }
        }
    }
}
