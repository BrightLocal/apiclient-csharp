using Brightlocal.Exceptions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Brightlocal
{
    public class Api
    {
        //private const string JSON_FORMAT = "application/json";
        /** expiry can't be more than 30 minutes (1800 seconds) */
        private const int MAX_EXPIRY = 1800;
        /** API endpoint URL */
        private readonly Uri endpoint = new Uri("https://tools.brightlocal.com/seo-tools/api");
        private readonly string apiKey;
        private readonly string apiSecret;

        // create an instance of restsharp client
        private readonly RestClient client = new RestClient();


        public Api(string apiKey, string apiSecret, string endpoint = null)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            if (endpoint != null && Uri.IsWellFormedUriString(endpoint, UriKind.Absolute))
            {
                this.endpoint = new Uri(endpoint);
            }
            // set base url   
            client.BaseUrl = this.endpoint;
        }

        public Response Get(string resource, Parameters parametrs = null)
        {
            return Call(resource, parametrs, Method.GET);
        }

        public Response Post(string resource, Parameters parametrs = null)
        {
            return Call(resource, parametrs, Method.POST);
        }

        public Response Delete(string resource, Parameters parametrs = null)
        {
            return Call(resource, parametrs, Method.DELETE);
        }

        public Response Put(string resource, Parameters parametrs = null)
        {
            return Call(resource, parametrs, Method.PUT);
        }

        public Batch CreateBatch(bool stopOnJobError = false, string callbackUrl = null)
        {
            Parameters parametrs = new Parameters
            {
                { "stop-on-job-error", stopOnJobError }
            };
            if (callbackUrl != null)
            {
                parametrs.Add("callback", callbackUrl);
            }
            Response response = Post("/v4/batch", parametrs);
            if (!response.IsSuccess())
            {
                throw new CreateBatchException("An error occurred and we weren\'t able to create the batch. " + response.GetContent(), new Exception());
            }
            return new Batch(this, (int)response.GetContent()["batch-id"]);
        }

        public Batch GetBatch(int batchId)
        {
            return new Batch(this, batchId);
        }

        public Response PostImage(string resource, string filePath)
        {
            double expires = CreateExpiresParameter();

            // Generate encoded signature
            string signature = CreateSignature(apiKey, apiSecret, expires);
            // Generate the request
            RestRequest request = new RestRequest(resource, Method.POST);
            request.AddParameter("api-key", apiKey);
            request.AddParameter("sig", signature);
            request.AddParameter("expires", expires);
            request.AddFile("file", filePath);
            // execure the request
            IRestResponse response = client.Execute(request);
            // deserialize the response
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new GeneralException(response.ErrorMessage, new Exception());
            }
            return new Response(response);
        }

        private Response Call(string resource, Parameters parametrs, Method method)
        {
            double expires = CreateExpiresParameter();
           
            // Generate encoded signature
            string signature = CreateSignature(apiKey, apiSecret, expires);
            // Generate the request
            RestRequest request = GetApiRequest(method, resource, apiKey, signature, expires, parametrs);
            // execure the request
            IRestResponse response = client.Execute(request, method);
            // deserialize the response
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new GeneralException(response.ErrorMessage, new Exception());
            }
            return new Response(response);
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
                return Convert.ToBase64String(hashmessage);
            }
        }

        // Create expires paramater for signature and api requests
        private static double CreateExpiresParameter()
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc); // The seconds since the Unix Epoch (January 1 1970 00:00:00 GMT)
            TimeSpan diff = (DateTime.Now).ToUniversalTime() - origin;  // Subtract the seconds since the Unix Epoch from today's date. 
            return Math.Floor(diff.TotalSeconds + MAX_EXPIRY); // Not more than 1800 seconds
        }

        private static RestRequest GetApiRequest(Method method, string url, string apiKey, string sig, double expires, Parameters parameters)
        {
            RestRequest request = new RestRequest(url, method);
            if (parameters == null)
            {
                parameters = new Parameters();
            }
            parameters.Add("api-key", apiKey);
            parameters.Add("sig", sig);
            parameters.Add("expires", expires);

            if (method == Method.GET)
            {
                foreach (KeyValuePair<string, object> kvp in parameters)
                {
                    request.AddParameter(kvp.Key, kvp.Value);
                }
            }
            else
            {
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(parameters);
            }
            return request;
        }
    }
}
