using Microsoft.Extensions.Configuration;
using Xunit;

namespace TeamSupport.NET.SDK.Tests
{
    public class TeamSupportServiceClientTests
    {
        const string SERVER_NAME = "na2";

        private static IConfigurationRoot GetConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("client-secrets.json")
                .Build();

            return config;
        }

        private static string GetApiToken()
        {
            var config = GetConfig();
            return config["ORGANIZATION_ID"];
        }

        private static string GetOrganizationId()
        {
            var config = GetConfig();
            return config["API_TOKEN"];
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
