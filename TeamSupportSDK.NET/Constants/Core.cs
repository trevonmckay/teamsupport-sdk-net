namespace TeamSupportSDK.NET.Constants
{
    public static class Core
    {
        public static class Headers
        {
            public const string Bearer = "Bearer";

            public const string FormUrlEncodedContentType = "application/x-www-form-urlencoded";

            public const string ThrowSiteHeaderName = "X-ThrowSite";
        }

        public static class HTTPMethods
        {
            public const string GET = "GET";

            public const string POST = "POST";

            public const string DELETE = "DELETE";

            public const string PATCH = "PATCH";

            public const string PUT = "PUT";

            public const string OPTIONS = "OPTIONS";
        }

        public static class Serialization
        {
            public const string ODataType = "@odata.type";
        }
    }
}
