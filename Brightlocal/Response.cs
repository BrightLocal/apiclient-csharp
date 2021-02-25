using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;

namespace Brightlocal
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
            try
            {
                return JsonConvert.DeserializeObject(response.Content);
            } catch (Exception)
            {
                return response.Content;
            }
            
        }

        public bool IsSuccess()
        {
            dynamic content = GetContent();
            if (content.success != null)
            {
                return (bool)content.success;
            }

            if (content.errors != null)
            {
                return false;
            }
            return successResponseCodes.Contains(GetResponseCode());
        }
    }
}
