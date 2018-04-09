using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TeamSupportSDK.NET.Providers
{
    public class DefaultAuthenticationProvider : IAuthenticationProvider
    {
        private string _apiToken;

        public string OrganizationId { get; set; }

        public DefaultAuthenticationProvider(string organizationId, string apiToken)
        {
            _apiToken = apiToken;
            this.OrganizationId = organizationId;
        }

        public Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            string credentials = string.Format("{0}:{1}", this.OrganizationId, _apiToken);
            Byte[] credentialsByteArray = Encoding.UTF8.GetBytes(credentials);
            string encodedCredentials = Convert.ToBase64String(credentialsByteArray);
            request.Headers.Add("Authorization", "Basic " + encodedCredentials);

            return Task.FromResult(0);
        }
    }
}
