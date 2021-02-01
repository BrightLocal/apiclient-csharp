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
            Parametrs parameters = new Parametrs();
            parameters.Add("batch-id", id);
            dynamic response = api.Put("v4/batch", parameters);
            dynamic content = JsonConvert.DeserializeObject(response.Content);
            if (content.success != "true")
            {
                throw new BatchNotCommitedException("An error occurred and we aren\'t able to commit the batch. " + content.errors, content.ErrorException);
            }
            return true;
        }
        public dynamic GetResults()
        {
            Parametrs parameters = new Parametrs();
            parameters.Add("batch-id", id);
            dynamic response = api.Get("/v4/batch", parameters);
            dynamic obj = JsonConvert.DeserializeObject(response.Content);
            if (obj.success != "true")
            {
                throw new BatchNotCommitedException("An error occurred and we aren\'t able to commit the batch." + obj.errors, obj.ErrorException);
            }
            return obj;
        }
    }
}
