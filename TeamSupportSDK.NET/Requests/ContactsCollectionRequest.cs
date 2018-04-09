using System.Collections.Generic;
using System.Threading.Tasks;
using TeamSupportSDK.NET.Models;
using TeamSupportSDK.NET.Providers;
using TeamSupportSDK.NET.SDK.Requests;

namespace TeamSupportSDK.NET.Requests
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
