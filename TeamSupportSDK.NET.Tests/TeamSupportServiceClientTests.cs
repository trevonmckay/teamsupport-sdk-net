using Microsoft.Extensions.Configuration;
using System.Linq;
using Xunit;

namespace TeamSupportSDK.NET.Tests
{
    public class TeamSupportServiceClientTests
    {
        const string SERVER_NAME = "na2";
        const string ENVIRONMENT = "Development";

        private static IConfigurationRoot GetConfig(string env = "Production")
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env}.json", true)
                .Build();

            return config;
        }

        private static string GetApiToken()
        {
            var config = GetConfig(ENVIRONMENT);
            return config["API_TOKEN"];
        }

        private static string GetOrganizationId()
        {
            var config = GetConfig(ENVIRONMENT);
            return config["ORGANIZATION_ID"];
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
