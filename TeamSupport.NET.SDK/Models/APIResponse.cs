using System;
using System.Collections.Generic;

namespace TeamSupport.NET.SDK.Models
{
    public class RequestResponse
    {
        public Contact Contact { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }
    }
}
