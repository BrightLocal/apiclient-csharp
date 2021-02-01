using Newtonsoft.Json;
using RestSharp;

namespace Brightlocal
{
    public class ApiResponse : RestResponse
    {
        public dynamic GetResult()
        {
            return JsonConvert.DeserializeObject(Content);
        }
    }
}
