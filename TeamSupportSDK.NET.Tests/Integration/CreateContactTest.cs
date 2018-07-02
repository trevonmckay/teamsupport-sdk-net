using System.Linq;
using TeamSupportSDK.NET.Models;
using Xunit;

namespace TeamSupportSDK.NET.Tests.Integration
{
    public class CreateContactTest : BaseTestClass
    {
        [Fact]
        public async System.Threading.Tasks.Task ShouldReturnNewContactAsync()
        {
            var defaultAuthenticationProvider = new Providers.DefaultAuthenticationProvider(GetOrganizationId(), GetApiToken());
            var tsClient = new TeamSupportServiceClient(SERVER_NAME, defaultAuthenticationProvider);

            var allOrganizations = await tsClient.Customers.Request().GetAsync();
            var defaultOrganization = allOrganizations.First();

            var newContact = new Contact()
            {
                AdditionalData = new System.Collections.Generic.Dictionary<string, object>()
                {
                    { "PPR", "123456700" },
                    { "Division", "CORP" },
                    { "Department", "Technology" },
                    { "Station", "ATL" }
                },
                Email = "support.user@test.com",
                FirstName = "Johnny",
                LastName = "Appleseed",
                IsPortalUser = true,
                Organization = "Sandbox",
                OrganizationId = defaultOrganization.OrganizationID,
                Title = "API Generated User"
            };

            var result = await tsClient.Contacts.Request().AddAsync(newContact);

            Assert.NotNull(result);
        }
    }
}
