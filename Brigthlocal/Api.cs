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


        public IRestResponse Get(string resource, Parametrs parametrs)
        {
            return Call(resource, parametrs, Method.GET);
        }

        public IRestResponse Post(string resource, Parametrs parametrs)
        {
            return Call(resource, parametrs, Method.POST);
        }

        public IRestResponse Delete(string resource, Parametrs parametrs)
        {
            return Call(resource, parametrs, Method.DELETE);
        }

        public IRestResponse Put(string resource, Parametrs parametrs)
        {
            return Call(resource, parametrs, Method.PUT);
        }

        public Batch CreateBatch(bool stopOnJobError = false, string callbackUrl = null)
        {
            Parametrs parametrs = new Parametrs();
            parametrs.Add("stop-on-job-error", stopOnJobError);
            if (callbackUrl != null)
            {
                parametrs.Add("callback", callbackUrl);
            }
            IRestResponse response = Post("/v4/batch", parametrs);
            dynamic content = JsonConvert.DeserializeObject(response.Content);
            if (content.success != true)
            {
                const string message = "Error creating Batch ";
                var batchException = new ApplicationException(message + content.errors, content.ErrorException);
                throw batchException;
            }
            return new Batch(this, (int)content["batch-id"]);
        }

        public Batch GetBatch(int batchId)
        {
            return new Batch(this, batchId);
        }

        private IRestResponse Call(string resource, Parametrs parametrs, Method method)
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
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new ApplicationException(response.ErrorMessage);
            }
            return response;
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

        private static RestRequest GetApiRequest(Method method, string url, string apiKey, string sig, double expires, Dictionary<string, object> parameters)
        {
            // Create a new restsharp request
            RestRequest request = new RestRequest("https://maksmdkmaskdmka.com", method);
            // Add appropriate headers to request
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            // Add key, sig and expires to request
            request.AddParameter("api-key", apiKey);
            request.AddParameter("sig", sig);
            request.AddParameter("expires", expires);

            // Loop through the parameters passed in as a dictionary and add each one to a dynamic object
            var eo = new ExpandoObject();
            var eoColl = (ICollection<KeyValuePair<string, object>>)eo;
            foreach (var kvp in parameters)
            {
                eoColl.Add(kvp);
            }
            dynamic eoDynamic = eo;

            // Add each parameter to restsharp request
            foreach (var prop in eoDynamic)
            {
                request.AddParameter(prop.Key, prop.Value);
            }
            return request;
        }
    }
}
