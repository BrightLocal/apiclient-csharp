using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brigthlocal
{
    public class Response
    {
        public IRestResponse response;
        private readonly int[] successResponseCodes = { 200, 201 };
        public Response(IRestResponse response)
        {
            this.response = response;
        }

        public int GetResponseCode()
        {
            return (int)response.StatusCode;
        }

        public dynamic GetContent()
        {
            return JsonConvert.DeserializeObject(response.Content);
        }

        public bool IsSuccess()
        {
            dynamic content = GetContent();
            if (content.success != null)
            {
                return (bool)content.success;
            }
            return successResponseCodes.Contains(GetResponseCode());
        }
    }
}
