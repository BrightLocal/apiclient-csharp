using Brightlocal;
using Brightlocal.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Examples
{
    class OffsiteSeoExample
    {

        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'fetch offsite seo information' or 1. Plese enter a command. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command.Trim())
                {
                    case "fetch offsite seo information":
                    case "1":
                        OffsiteSeo(apiKey, apiSecret);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
            } while (true);
        }

        private static void OffsiteSeo(string apiKey, string apiSecret)
        {
            List<string> directories = new List<string>
            {
                "http://www.gramercytavern.com/",
                "https://bodegawinebar.com/"
            };

            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.CreateBatch();
            Console.WriteLine("Created batch ID {0}", batch.GetId());
            foreach (string directory in directories)
            {
                Parameters parameters = new Parameters
                {
                    { "website-url", directory }
                };
                try
                {
                    // Add jobs to batch
                    dynamic jobResponse = batch.AddJob("/v4/seo/offsite", parameters);
                    Console.WriteLine("Added job with ID {0}", jobResponse["job-id"]);
                }
                catch (GeneralException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            batch.Commit();
            Console.WriteLine("Batch committed successfully, awaiting results.");
            dynamic response;
            do
            {
                Thread.Sleep(5000);
                response = batch.GetResults();
            } while (!(new List<string> { "Stopped", "Finished" }).Contains((string)response.status));
            Console.WriteLine(response);
        }

    }
}
