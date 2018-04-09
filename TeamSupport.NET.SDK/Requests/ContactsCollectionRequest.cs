using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamSupport.NET.SDK.Models;
using TeamSupport.NET.SDK.Providers;

namespace TeamSupport.NET.SDK.Requests
{
    public class ContactsCollectionRequest : BaseRequest
    {
        public ContactsCollectionRequest(string requestUrl, IBaseClient client) : base(requestUrl, client) { }

        /// <summary>
        /// Adds the specified Contact to the collection via POST
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>The created Contact.</returns>
        public async Task<object> AddAsync(Contact contact)
        {
            this.ContentType = "application/json";
            this.Method = "POST";

            return await this.SendAsync<Contact>(contact);
        }

        /// <summary>
        /// Gets the collection of Contacts.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Contact}"/> of <see cref="Contact"/>.</returns>
        public async Task<IEnumerable<Contact>> GetAsync()
        {
            this.Method = "GET";
            var response = await this.SendAsync<RequestResponse>(null);

            return response.Contacts;
        }
    }
}
