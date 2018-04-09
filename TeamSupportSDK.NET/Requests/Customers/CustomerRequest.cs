using System.Threading.Tasks;
using TeamSupportSDK.NET.Models;
using TeamSupportSDK.NET.Providers;

namespace TeamSupportSDK.NET.Requests
{
    public class CustomerRequest : BaseRequest
    {
        public CustomerRequest(string requestUrl, IBaseClient client) : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Gets the specified Customer
        /// </summary>
        /// <returns>The <see cref="Customer"/>.</returns>
        public async Task<Customer> GetAsync()
        {
            this.Method = Constants.Core.HTTPMethods.GET;
            var response = await this.SendAsync<RequestResponse>(null);

            return response.Customer;
        }
    }
}
