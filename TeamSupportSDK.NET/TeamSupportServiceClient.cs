using TeamSupportSDK.NET.Providers;
using TeamSupportSDK.NET.Requests;

namespace TeamSupportSDK.NET
{
    public class TeamSupportServiceClient : BaseClient, IBaseClient
    {
        public string ServerName { get; private set; }

        public TeamSupportServiceClient(string serverName, IAuthenticationProvider authenticationProvider)
            : base(serverName, authenticationProvider) { }

        public ContactsCollectionRequestBuilder Contacts
        {
            get
            {
                return new ContactsCollectionRequestBuilder(this.BaseUrl + "/json/contacts", this);
            }
        }
    }
}
