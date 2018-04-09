using System;
using System.Collections.Generic;
using System.Text;
using TeamSupport.NET.SDK.Models;

namespace TeamSupport.NET.SDK
{
    public class ServiceException : Exception
    {
        public ServiceException(Error error) { }
    }
}
