using Brightlocal;
using System;
namespace Examples
{
    class BatchExample
    {
        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'create' or 1, 'commit' or 2, 'get result' or 3, 'delete' or 4, 'stop' or 5. Plese enter a command. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command.Trim())
                {
                    case "create":
                    case "1":
                        CreateBatch(apiKey, apiSecret);
                        break;
                    case "commit":
                    case "2":
                        int batchId = Program.GetIntegerValue("Enter batch id that you want to commit");
                        CommitBatch(apiKey, apiSecret, batchId);
                        break;
                    case "get result":
                    case "3":
                        int batchIdForGetResult = Program.GetIntegerValue("Enter batch id from what you want to get result");
                        GetResult(apiKey, apiSecret, batchIdForGetResult);
                        break;
                    case "delete":
                    case "4":
                        int batchIdForDelete = Program.GetIntegerValue("Enter batch id that you want to delete");
                        Delete(apiKey, apiSecret, batchIdForDelete);
                        break;
                    case "stop":
                    case "5":
                        int batchIdForStop = Program.GetIntegerValue("Enter batch id that you want to stop");
                        Delete(apiKey, apiSecret, batchIdForStop);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
            } while (true);
        }

        public static void CreateBatch(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.CreateBatch();
            Console.WriteLine("Created batch ID '{0}'", batch.GetId());
        }

        public static void CommitBatch(string apiKey, string apiSecret, int batchId)
        {
            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.GetBatch(batchId);
            batch.Commit();
            Console.WriteLine("Batch committed successfully");
        }

        public static void GetResult(string apiKey, string apiSecret, int batchId)
        {
            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.GetBatch(batchId);
            Console.WriteLine(batch.GetResults());            
        }

        public static void Delete(string apiKey, string apiSecret, int batchId)
        {
            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.GetBatch(batchId);
            batch.Delete();
            Console.WriteLine("Batch deleted successfully");
        }

        public static void Stop(string apiKey, string apiSecret, int batchId)
        {
            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.GetBatch(batchId);
            batch.Stop();
            Console.WriteLine("Batch stoped successfully");
        }
    }
}
