using System.Collections.Generic;

namespace TeamSupportSDK.NET.Models
{
    public class ErrorResponse
    {
        public Error Error { get; set; }

        public IDictionary<string, object> AdditionalData { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public string ThrowSite { get; set; }

        public Error InnerError { get; set; }

        public IDictionary<string, object> AdditionalData { get; set; }

        public Error() { }

        public Error(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }
}
