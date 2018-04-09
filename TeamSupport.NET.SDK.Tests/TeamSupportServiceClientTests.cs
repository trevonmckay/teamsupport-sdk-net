using System;
using Xunit;

namespace TeamSupport.NET.SDK.Tests
{
    public class TeamSupportServiceClientTests
    {
        const string SERVER_NAME = "na2";
        const string ORGANIZATION_ID = "1037178";
        const string API_TOKEN = "06f2b6d5-2dee-4cec-82a4-bf29e2d76c60";

        [Fact]
        public async void GetAllContactsAsync_Success()
        {
            var defaultAuthenticationProvider = new Providers.DefaultAuthenticationProvider(ORGANIZATION_ID, API_TOKEN);
            var tsClient = new TeamSupportServiceClient(SERVER_NAME, defaultAuthenticationProvider);
            var contacts = await tsClient.Contacts.Request().GetAsync();

            Assert.NotEmpty(contacts);
        }

        [Fact]
        public async void CreateContactAsync_Success()
        {
            var defaultAuthenticationProvider = new Providers.DefaultAuthenticationProvider(ORGANIZATION_ID, API_TOKEN);
            var tsClient = new TeamSupportServiceClient(SERVER_NAME, defaultAuthenticationProvider);
            var newContact = new Models.Contact()
            {
                Email = "support.user@globalservices.com",
                FirstName = "Johnny",
                LastName = "Appleseed",
                IsPortalUser = true,
                PPR = "000000901",
                Title = "API Generated User",
                OrganzationId = ORGANIZATION_ID
            };

            var result = await tsClient.Contacts.Request().AddAsync(newContact);
            Assert.IsType<Models.Contact>(result);
        }
    }
}
