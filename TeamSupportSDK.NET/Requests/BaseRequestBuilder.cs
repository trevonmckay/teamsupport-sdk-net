using TeamSupportSDK.NET.Providers;

namespace TeamSupportSDK.NET.Requests
{
    public class BaseRequestBuilder
    {
        public IBaseClient Client { get; private set; }

        public string RequestUrl { get; set; }

        public BaseRequestBuilder(string requestUrl, IBaseClient client)
        {
            this.Client = client;
            this.RequestUrl = requestUrl;
        }

        public string AppendSegmentToRequestUrl(string urlSegment)
        {
            var baseUrl = this.RequestUrl.TrimEnd('/');
            urlSegment = urlSegment.TrimStart('/');

            return string.Format("{0}/{1}", baseUrl, urlSegment);
        }
    }
}
