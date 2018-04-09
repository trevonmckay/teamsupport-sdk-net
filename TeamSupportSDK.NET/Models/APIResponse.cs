using System.Collections.Generic;

namespace TeamSupportSDK.NET.Models
{
    public class RequestResponse
    {
        public Contact Contact { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }
    }
}
