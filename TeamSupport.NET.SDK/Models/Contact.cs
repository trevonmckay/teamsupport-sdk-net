using System;
using System.Collections.Generic;
using System.Text;

namespace TeamSupport.NET.SDK.Models
{
    public class Contact
    {
        public string Id { get; set; }

        public string OrganzationId { get; set; }

        public bool IsPortalUser { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Organization { get; set; }

        public string Title { get; set; }
    }
}
