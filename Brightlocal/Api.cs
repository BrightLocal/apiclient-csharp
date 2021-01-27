using System;
using System.Collections.Generic;

namespace Brightlocal
{
    public class Api
    {
        /** API endpoint URL */
        const string ENDPOINT = "https://tools.brightlocal.com/seo-tools/api";
        /** expiry can't be more than 30 minutes (1800 seconds) */
        const int MAX_EXPIRY = 1800;

        private Uri endpoint = new Uri("https://tools.brightlocal.com/seo-tools/api/");
        private string apiKey;
        private string apiSecret;

        private enum Methods
        {
            get,
            post,
            put,
            delete
        }

        public Api(string apiKey, string apiSecret, string endpoint = null)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            if (endpoint != null)
            {
                this.endpoint = new Uri(endpoint);
            }

        }


        public ApiResponse Get(string resource, Parametrs parametrs)
        {
            return Call(resource, parametrs, Methods.get);
        }

        private ApiResponse Call(string resource, Parametrs parametrs, Methods metod)
        {
            return new ApiResponse(200, new Dictionary<string, object> { });
        }

        /*  public function post(string $resource, array $params = []): ApiResponse {
              return $this->call($resource, $params);
          }

          public function put(string $resource, array $params = []): ApiResponse {
              return $this->call($resource, $params, Methods::PUT);
          }

          public function delete(string $resource, array $params = []): ApiResponse {
              return $this->call($resource, $params, Methods::DELETE);
          }
      */

        public string Test()
        {
            return "It's work 12";
        }
    }
}
