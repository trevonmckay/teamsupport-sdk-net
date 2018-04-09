using Microsoft.Extensions.Configuration;

namespace TeamSupportSDK.NET.Tests
{
    public class BaseTestClass
    {
        public const string SERVER_NAME = "na2";
        public const string ENVIRONMENT = "Development";

        protected static IConfigurationRoot GetConfig(string env = "Production")
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env}.json", true)
                .Build();

            return config;
        }

        protected static string GetApiToken()
        {
            var config = GetConfig(ENVIRONMENT);
            return config["API_TOKEN"];
        }

        protected static string GetOrganizationId()
        {
            var config = GetConfig(ENVIRONMENT);
            return config["ORGANIZATION_ID"];
        }
    }
}
