using Brightlocal;
using Brigthlocal.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples
{
    class LocalDirectoriesExamples
    {
        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'fetch profile url' or 1, 'fetch profile url by telephone' or 2, 'fetch profile details' or 3, 'fetch profile details by business data' or 4. Plese enter a command. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command.Trim())
                {
                    case "fetch profile url":
                    case "1":
                        FetchProfileUrl(apiKey, apiSecret);
                        break;
                    case "fetch profile url by telephone":
                    case "2":
                        FetchProfileUrlByTelephone(apiKey, apiSecret);
                        break;
                    case "fetch profile details":
                    case "3":
                        FetchProfileDetails(apiKey, apiSecret);
                        break;
                    case "fetch profile details by business data":
                    case "4":
                        FetchProfileDetailsByBusinessData(apiKey, apiSecret);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
            } while (true);
        }

        private static void FetchProfileUrl(string apiKey, string apiSecret)
        {
            List<string> localDirectories = new List<string>
            {
                "google",
                "yelp",
                "yahoo"
            };

            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.CreateBatch();
            Console.WriteLine("Created batch ID {0}", batch.GetId());
            foreach (string directory in localDirectories)
            {
                Parameters parameters = new Parameters
                {
                    { "business-names", "Eleven Madison Park" },
                    { "country", "USA" },
                    { "city", "New York" },
                    { "postcode", "10010" },
                    { "local-directory", directory }
                };
                try
                {
                    // Add jobs to batch
                    dynamic jobResponse = batch.AddJob("/v4/ld/fetch-profile-url", parameters);
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

        private static void FetchProfileUrlByTelephone(string apiKey, string apiSecret)
        {
            List<string> localDirectories = new List<string>
            {
                "google",
                "facebook",
            };

            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.CreateBatch();
            Console.WriteLine("Created batch ID {0}", batch.GetId());
            foreach (string directory in localDirectories)
            {
                Parameters parameters = new Parameters
                {
                    { "search-type", "search-by-phone" },
                    { "telephone", "+1 212-829-0812" },
                    { "local-directory", directory }
                };
                try
                {
                    // Add jobs to batch
                    dynamic jobResponse = batch.AddJob("/v4/ld/fetch-profile-url", parameters);
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

        private static void FetchProfileDetails(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.CreateBatch();
            Console.WriteLine("Created batch ID {0}", batch.GetId());
            Parameters parameters = new Parameters
                {
                    { "profile-url", "https://local.google.com/place?id=16045207703484290935&use=srp&hl=en" },
                    { "country", "USA" },
                };
            try
            {
                // Add jobs to batch
                dynamic jobResponse = batch.AddJob("/v4/ld/fetch-profile-details", parameters);
                Console.WriteLine("Added job with ID {0}", jobResponse["job-id"]);
            }
            catch (GeneralException exception)
            {
                Console.WriteLine(exception.Message);
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

        private static void FetchProfileDetailsByBusinessData(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.CreateBatch();
            Console.WriteLine("Created batch ID {0}", batch.GetId());
            List<string> localDirectories = new List<string>
            {
                "google",
                "yelp",
                "yahoo"
            };
            foreach (string directory in localDirectories)
            {
                Parameters parameters = new Parameters
                {
                    { "business-names", "Eleven Madison Park" },
                    { "country", "USA" },
                    { "city", "New York" },
                    { "postcode", "10010" },
                    { "local-directory", directory }
                };
                try
                {
                    // Add jobs to batch
                    dynamic jobResponse = batch.AddJob("/v4/ld/fetch-profile-details-by-business-data", parameters);
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
