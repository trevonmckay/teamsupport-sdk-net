using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamSupport.NET.SDK.Models;
using TeamSupport.NET.SDK.Providers;

namespace TeamSupport.NET.SDK.Requests
{
    public class ContactRequest : BaseRequest
    {
        public ContactRequest(string requestUrl, IBaseClient client) : base(requestUrl, client) { }

        /// <summary>
        /// Gets the specified Contact.
        /// </summary>
        /// <returns>The <see cref="Contact"/>.</returns>
        public async Task<Contact> GetAsync()
        {
            this.Method = "GET";
            var response = await this.SendAsync<RequestResponse>(null);

            return response.Contact;
        }
    }
}
