using Brigthlocal.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brightlocal
{
    public class Batch
    {
        private Api api;
        private int id;

        public Batch(Api batchApi, int batchId)
        {
            api = batchApi;
            id = batchId;
        }

        public int GetId()
        {
            return id;
        }

        public bool Commit()
        {
            Parameters parameters = new Parameters();
            parameters.Add("batch-id", id);
            IRestResponse response = api.Put("v4/batch", parameters);
            dynamic content = JsonConvert.DeserializeObject(response.Content);
            if (content.success != "true")
            {
                throw new BatchNotCommitedException("An error occurred and we aren\'t able to commit the batch. " + content.errors, content.ErrorException);
            }
            return true;
        }
        public dynamic GetResults()
        {
            Parameters parameters = new Parameters();
            parameters.Add("batch-id", id);
            IRestResponse response = api.Get("/v4/batch", parameters);
            dynamic obj = JsonConvert.DeserializeObject(response.Content);
            if (obj.success != "true")
            {
                throw new BatchNotCommitedException("An error occurred and we aren\'t able to commit the batch." + obj.errors, obj.ErrorException);
            }
            return obj;
        }
        
        public bool Delete()
        {
            Parameters parameters = new Parameters();
            parameters.Add("batch-id", id);
            IRestResponse response = api.Delete("/v4/batch", parameters);
            dynamic obj = JsonConvert.DeserializeObject(response.Content);
            if (obj.success != true)
            {
                throw new BatchNotCommitedException("An error occurred and we aren\'t able to delete the batch." + obj.errors, obj.ErrorException);
            }
            return true;
        }

        public bool Stop()
        {
            Parameters parameters = new Parameters();
            parameters.Add("batch-id", id);
            IRestResponse response = api.Put("/v4/batch/stop", parameters);
            dynamic obj = JsonConvert.DeserializeObject(response.Content);
            if (obj.success != true)
            {
                throw new GeneralException("An error occurred and we aren\'t able to stop the batch." + obj.errors, obj.ErrorException);
            }
            return true;
        }
        public dynamic AddJob(string resource, Parameters parameters)
        {
            parameters.Add("batch-id", id);
            IRestResponse response = api.Post(resource, parameters);
            dynamic obj = JsonConvert.DeserializeObject(response.Content);
            if (obj.success != true)
            {
                throw new GeneralException("An error occurred and we aren\'t able to add job to the batch." + obj.errors, obj.ErrorException);
            }
            return obj;
        }
    }
}
