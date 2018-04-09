using System;
using System.Collections.Generic;
using System.Text;
using TeamSupport.NET.SDK.Providers;

namespace TeamSupport.NET.SDK.Requests
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
