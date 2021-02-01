using Brightlocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class BatchExample
    {

        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'create', 'commit', 'get result', 'delete', 'stop'. Plese enter a command. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "create":
                        CreateBatch(apiKey, apiSecret);
                        break;
                    case "commit":
                        int batchId = Program.GetIntegerValue("Enter batch id that you want to commit");
                        CommitBatch(apiKey, apiSecret, batchId);
                        break;
                    case "get result":
                        Console.WriteLine("Enter batch id from what you want to get result");
                        int batchIdForGetResult = Convert.ToInt32(Console.ReadLine());
                        GetResult(apiKey, apiSecret, batchIdForGetResult);
                        break;
                    case "delete":
                        Console.WriteLine("Enter batch id that you want to delete");
                        int batchIdForDelete = Convert.ToInt32(Console.ReadLine());
                        Delete(apiKey, apiSecret, batchIdForDelete);
                        break;
                    case "stop":
                        Console.WriteLine("Enter batch id that you want to delete");
                        int batchIdForStop = Convert.ToInt32(Console.ReadLine());
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
            batch.Commit();
            Console.WriteLine("Batch committed successfully");
        }
    }
}
