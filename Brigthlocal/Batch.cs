using Brigthlocal;
using Brigthlocal.Exceptions;

namespace Brightlocal
{
    public class Batch
    {
        private readonly Api api;
        private readonly int id;

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
            Response response = api.Put("v4/batch", parameters);
            if (!response.IsSuccess())
            {
                throw new BatchNotCommitedException("An error occurred and we aren\'t able to commit the batch. " + response.GetContent().errors, new System.Exception());
            }
            return true;
        }

        public dynamic GetResults()
        {
            Parameters parameters = new Parameters();
            parameters.Add("batch-id", id);
            Response response = api.Get("/v4/batch", parameters);
            if (!response.IsSuccess())
            {
                throw new GetResultException("An error occurred and we aren\'t able to get the batch result." + response.GetContent().errors, new System.Exception());
            }
            return response.GetContent();
        }

        public bool Delete()
        {
            Parameters parameters = new Parameters();
            parameters.Add("batch-id", id);
            Response response = api.Delete("/v4/batch", parameters);
            if (!response.IsSuccess())
            {
                throw new BatchNotCommitedException("An error occurred and we aren\'t able to delete the batch." + response.GetContent().errors, new System.Exception());
            }
            return true;
        }

        public bool Stop()
        {
            Parameters parameters = new Parameters
            {
                { "batch-id", id }
            };
            Response response = api.Put("/v4/batch/stop", parameters);
            if (!response.IsSuccess())
            {
                throw new StopBatchExeption("An error occurred and we aren\'t able to stop the batch." + response.GetContent().errors, new System.Exception());
            }
            return true;
        }

        public dynamic AddJob(string resource, Parameters parameters)
        {
            parameters.Add("batch-id", id);
            Response response = api.Post(resource, parameters);
            if (!response.IsSuccess())
            {
                throw new AddJobException("An error occurred and we aren\'t able to add job to the batch." + response.GetContent().errors, new System.Exception());
            }
            return response.GetContent();
        }
    }
}
