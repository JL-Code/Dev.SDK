using System.Net;

namespace Dev.SDK
{
    public class ResponseData
    {
        public object Content { get; set; }
        public string Message { get; set; }
        public bool IsSuccess
        {
            get
            {
                return StatusCode == HttpStatusCode.OK;
            }
        }
        public HttpStatusCode StatusCode { get; set; }
    }
}