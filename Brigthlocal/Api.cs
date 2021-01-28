using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography;
using System.Web;

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

        // create an instance of restsharp client
        RestClient client = new RestClient();


        public Api(string apiKey, string apiSecret, string endpoint = null)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;          
            if (endpoint != null && Uri.IsWellFormedUriString(endpoint, UriKind.Absolute))
            {
                this.endpoint = new Uri(endpoint);
            }
        }


        public dynamic Get(string resource, Parametrs parametrs)
        {
            return Call(resource, parametrs, Method.GET);
        }

        public dynamic Post(string resource, Parametrs parametrs)
        {
            return Call(resource, parametrs, Method.POST);
        }

        public dynamic Delete(string resource, Parametrs parametrs)
        {
            return Call(resource, parametrs, Method.DELETE);
        }

        public dynamic Put(string resource, Parametrs parametrs)
        {
            return Call(resource, parametrs, Method.PUT);
        }

        private dynamic Call(string resource, Parametrs parametrs, Method method)
        {

            double expires = CreateExpiresParameter();
            // set base url   
            client.BaseUrl = endpoint;

            // Generate encoded signature
            string signature = CreateSignature(apiKey, apiSecret, expires);
            // Generate the request
            RestRequest request = GetApiRequest(method, resource, apiKey, signature, expires, parametrs);
            // execure the request
            IRestResponse response = client.Execute(request, method);
            // deserialize the response
            return JsonConvert.DeserializeObject(response.Content);            
        }


        // Create base 64 sha1 encrypted signature
        private static string CreateSignature(string apiKey, string secretKey, double expires)
        {
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secretKey);
            byte[] messageBytes = encoding.GetBytes(apiKey + expires);
            using (var hmacsha1 = new HMACSHA1(keyByte))
            {
                byte[] hashmessage = hmacsha1.ComputeHash(messageBytes);
                string signature = Convert.ToBase64String(hashmessage);
                return signature;
                /*return HttpUtility.UrlEncode(signature);*/
            }

        }

        // Create expires paramater for signature and api requests
        private static double CreateExpiresParameter()
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc); // The seconds since the Unix Epoch (January 1 1970 00:00:00 GMT)
            TimeSpan diff = (DateTime.Now).ToUniversalTime() - origin;  // Subtract the seconds since the Unix Epoch from today's date. 
            return Math.Floor(diff.TotalSeconds + MAX_EXPIRY); // Not more than 1800 seconds
        }

        private static RestRequest GetApiRequest(Method method, string url, string apiKey, string sig, double expires, Dictionary<string, object> apiParameters)
        {
            // Create a new restsharp request
            RestRequest request = new RestRequest(url, method);
            // Add appropriate headers to request
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            // Add key, sig and expires to request
            request.AddParameter("api-key", apiKey);
            request.AddParameter("sig", sig);
            request.AddParameter("expires", expires);

            /*
            request.Parameters.Add(new Parameter("api-key", apiKey, ParameterType.QueryStringWithoutEncode));
            request.Parameters.Add(new Parameter("expires", expires, ParameterType.QueryStringWithoutEncode));
            request.Parameters.Add(new Parameter("api-key", apiKey, ParameterType.QueryStringWithoutEncode));
            */

            /*dynamic o = new { apikey = apiKey };
             request.AddParameter(o);*/
            // Loop through the parameters passed in as a dictionary and add each one to a dynamic object
            // Loop through the parameters passed in as a dictionary and add each one to a dynamic object
            var eo = new ExpandoObject();
            var eoColl = (ICollection<KeyValuePair<string, object>>)eo;
            foreach (var kvp in apiParameters)
            {
                eoColl.Add(kvp);
            }
            dynamic eoDynamic = eo;

            // Add each parameter to restsharp request
            foreach (var prop in eoDynamic)
            {
              request.AddParameter(prop.Key, prop.Value);
            }  /**/

            return request;
        }
    }
}
