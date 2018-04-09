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
                Email = "support.user@globalservices.com",
                FirstName = "Johnny",
                LastName = "Appleseed",
                IsPortalUser = true,
                Organization = "DGS Dev Center",
                OrganzationId = defaultOrganization.OrganizationID,
                Title = "API Generated User"
            };

            var result = await tsClient.Contacts.Request().AddAsync(newContact);

            Assert.NotNull(result);
        }
    }
}
