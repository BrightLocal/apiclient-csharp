using Brightlocal;
using Brightlocal.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Examples
{
    class ReviewsExamples
    {
        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'fetch reviews by profile url' or 1, 'fetch reviews by business data' or 2. Plese enter a command. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command.Trim())
                {
                    case "fetch reviews by profile url":
                    case "1":
                        FetchReviewsByProfileUrl(apiKey, apiSecret);
                        break;
                    case "fetch reviews by business data":
                    case "2":
                        FetchReviewsByBusinessData(apiKey, apiSecret);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
            } while (true);
        }

        private static void FetchReviewsByProfileUrl(string apiKey, string apiSecret)
        {
            List<string> profileUrls = new List<string>
            {
               "https://local.google.com/place?id=2145618977980482902&use=srp&hl=en",
               "https://local.yahoo.com/info-27778787-le-bernardin-new-york",
               "https://www.yelp.com/biz/le-bernardin-new-york"
            };

            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.CreateBatch();
            Console.WriteLine("Created batch ID {0}", batch.GetId());
            foreach (string profileUrl in profileUrls)
            {
                Parameters parameters = new Parameters
                {
                    { "country", "USA" },
                    { "profile-url", profileUrl }
                };
                try
                {
                    // Add jobs to batch
                    dynamic jobResponse = batch.AddJob("/v4/ld/fetch-reviews", parameters);
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

        private static void FetchReviewsByBusinessData(string apiKey, string apiSecret)
        {
            List<string> localDirectories = new List<string>
            {
                "google",
                "yahoo",
            };

            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.CreateBatch();
            Console.WriteLine("Created batch ID {0}", batch.GetId());
            foreach (string directory in localDirectories)
            {
                Parameters parameters = new Parameters
                {
                    { "business-names", "Le Bernardin" },
                    { "country", "USA" },
                    { "street-address", "155 W 51st St" },
                    { "city", "New York" },
                    { "postcode", "10019" },
                    { "telephone", "(212) 554-1515" },
                    { "local-directory", directory }
                };
                try
                {
                    // Add jobs to batch
                    dynamic jobResponse = batch.AddJob("/v4/ld/fetch-reviews-by-business-data", parameters);
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
