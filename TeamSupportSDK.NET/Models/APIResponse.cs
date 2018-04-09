using System.Collections.Generic;

namespace TeamSupportSDK.NET.Models
{
    public class RequestResponse
    {
        public Customer Customer { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public Contact Contact { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }
    }
}
