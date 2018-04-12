using System.Collections.Generic;
using System.Threading.Tasks;
using TeamSupportSDK.NET.Models;
using TeamSupportSDK.NET.Providers;

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
        public async Task<Contact> AddAsync(Contact contact)
        {
            this.ContentType = "application/json";
            this.Method = Constants.Core.HTTPMethods.POST;

            var postWrapper = new
            {
                Contacts = contact
            };

            var result = await this.SendAsync<RequestResponse>(postWrapper);
            return result.Contact;
        }

        /// <summary>
        /// Gets the collection of Contacts.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Contact}"/> of <see cref="Contact"/>.</returns>
        public async Task<IEnumerable<Contact>> GetAsync()
        {
            this.Method = Constants.Core.HTTPMethods.GET;
            var response = await this.SendAsync<RequestResponse>(null);

            return response.Contacts;
        }
    }
}
