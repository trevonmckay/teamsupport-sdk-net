using System;
using System.Collections.Generic;
using System.Text;
using TeamSupportSDK.NET.Models;

namespace TeamSupportSDK.NET
{
    public class ServiceException : Exception
    {
        public ServiceException(Error error) { }
    }
}
