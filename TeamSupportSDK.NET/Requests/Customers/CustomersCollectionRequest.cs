using System.Collections.Generic;
using System.Threading.Tasks;
using TeamSupportSDK.NET.Models;
using TeamSupportSDK.NET.Providers;

namespace TeamSupportSDK.NET.Requests
{
    public class CustomersCollectionRequest : BaseRequest
    {
        public CustomersCollectionRequest(string requestUrl, IBaseClient client) : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Adds the specified Customer to the collection via POST
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>The created Customer.</returns>
        public async Task<object> AddAsync(Customer customer)
        {
            this.ContentType = "application/json";
            this.Method = Constants.Core.HTTPMethods.POST;

            return await this.SendAsync<Customer>(customer);
        }

        /// <summary>
        /// Gets the collection of Customers.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Customer}"/> of <see cref="Customer"/>.</returns>
        public async Task<IEnumerable<Customer>> GetAsync()
        {
            this.Method = Constants.Core.HTTPMethods.GET;
            var response = await this.SendAsync<RequestResponse>(null);

            return response.Customers;
        }
    }
}
