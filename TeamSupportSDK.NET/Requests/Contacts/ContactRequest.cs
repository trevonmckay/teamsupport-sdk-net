using System.Threading.Tasks;
using TeamSupportSDK.NET.Models;
using TeamSupportSDK.NET.Providers;

namespace TeamSupportSDK.NET.Requests
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
            this.Method = Constants.Core.HTTPMethods.GET;
            var response = await this.SendAsync<RequestResponse>(null);

            return response.Contact;
        }
    }
}
