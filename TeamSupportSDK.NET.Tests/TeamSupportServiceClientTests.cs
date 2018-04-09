using System.Linq;
using Xunit;

namespace TeamSupportSDK.NET.Tests
{
    public class TeamSupportServiceClientTests : BaseTestClass
    {
        [Fact]
        public async void GetAllCustomersAsync_Success()
        {
            var defaultAuthenticationProvider = new Providers.DefaultAuthenticationProvider(GetOrganizationId(), GetApiToken());
            var tsClient = new TeamSupportServiceClient(SERVER_NAME, defaultAuthenticationProvider);
            var customers = await tsClient.Customers.Request().GetAsync();

            Assert.NotEmpty(customers);
        }

        [Fact]
        public async void GetCustomerAsync_Success()
        {
            var defaultAuthenticationProvider = new Providers.DefaultAuthenticationProvider(GetOrganizationId(), GetApiToken());
            var tsClient = new TeamSupportServiceClient(SERVER_NAME, defaultAuthenticationProvider);
            var customers = await tsClient.Customers.Request().GetAsync();

            var customerId = customers.First().Id;
            var result = await tsClient.Customers[customerId].Request().GetAsync();

            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public async void GetAllContactsAsync_Success()
        {
            var defaultAuthenticationProvider = new Providers.DefaultAuthenticationProvider(GetOrganizationId(), GetApiToken());
            var tsClient = new TeamSupportServiceClient(SERVER_NAME, defaultAuthenticationProvider);
            var contacts = await tsClient.Contacts.Request().GetAsync();

            Assert.NotEmpty(contacts);
        }

        [Fact]
        public async void GetContactAsync_Success()
        {
            var defaultAuthenticationProvider = new Providers.DefaultAuthenticationProvider(GetOrganizationId(), GetApiToken());
            var tsClient = new TeamSupportServiceClient(SERVER_NAME, defaultAuthenticationProvider);
            var contacts = await tsClient.Contacts.Request().GetAsync();

            var contactId = contacts.First().Id;
            var result = await tsClient.Contacts[contactId].Request().GetAsync();

            Assert.Equal(contactId, result.Id);
        }

        [Fact]
        public async void CreateContactAsync_Success()
        {
            var defaultAuthenticationProvider = new Providers.DefaultAuthenticationProvider(GetOrganizationId(), GetApiToken());
            var tsClient = new TeamSupportServiceClient(SERVER_NAME, defaultAuthenticationProvider);
            var newContact = new Models.Contact()
            {
                Email = "support.user@test.com",
                FirstName = "Johnny",
                LastName = "Appleseed",
                IsPortalUser = true,
                Title = "API Generated User",
                OrganzationId = GetOrganizationId()
            };

            var result = await tsClient.Contacts.Request().AddAsync(newContact);
            Assert.IsType<Models.Contact>(result);
        }
    }
}
