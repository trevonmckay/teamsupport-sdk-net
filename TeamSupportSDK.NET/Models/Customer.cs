using Newtonsoft.Json;
using System;
using TeamSupportSDK.NET.Attributes;

namespace TeamSupportSDK.NET.Models
{
    public class Customer
    {
        public string Id { get; set; }

        public string OrganizationID
        {
            get
            {
                return this.Id;
            }

            set
            {
                this.Id = value;
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool HasPortalAccess { get; set; }

        public string Website { get; set; }

        [JsonIgnoreSerialization]
        [JsonProperty(PropertyName = "DateCreated")]
        public DateTime Created { get; set; }

        [JsonIgnoreSerialization]
        [JsonProperty(PropertyName = "DateModified")]
        public DateTime LastModified { get; set; }
    }
}
