using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamSupportSDK.NET.Models
{
    public class Customer
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool HasPortalAccess { get; set; }

        public string Website { get; set; }

        [JsonProperty(PropertyName = "DateCreated")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "DateModified")]
        public DateTime LastModified { get; set; }
    }
}
