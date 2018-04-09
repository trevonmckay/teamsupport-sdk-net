using System;
using System.Collections.Generic;
using System.Text;

namespace TeamSupport.NET.SDK.Constants
{
    public static class Core
    {
        public static class Headers
        {
            public const string Bearer = "Bearer";

            public const string FormUrlEncodedContentType = "application/x-www-form-urlencoded";

            public const string ThrowSiteHeaderName = "X-ThrowSite";
        }

        public static class Serialization
        {
            public const string ODataType = "@odata.type";
        }
    }
}
