using System;
using System.Collections.Generic;
using System.Text;

namespace TeamSupportSDK.NET.Providers
{
    public class BaseClient : IBaseClient
    {
        private string baseUrl;

        protected const string TEAM_SUPPORT_BASE_API_FORMAT = "https://app.{0}.teamsupport.com/api";

        public IAuthenticationProvider AuthenticationProvider { get; set; }

        public IHttpProvider HttpProvider { get; private set; }

        public string BaseUrl
        {
            get { return this.baseUrl; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Base URL cannot be null or empty.");
                }

                this.baseUrl = value.TrimEnd('/');
            }
        }

        public BaseClient(string serverName, IAuthenticationProvider authenticationProvider, IHttpProvider httpProvider = null)
        {
            this.BaseUrl = string.Format(TEAM_SUPPORT_BASE_API_FORMAT, serverName);
            this.AuthenticationProvider = authenticationProvider;
            this.HttpProvider = httpProvider ?? new DefaultHttpProvider();
        }
    }
}
