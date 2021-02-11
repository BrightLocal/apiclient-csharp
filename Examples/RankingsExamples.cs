﻿using Brightlocal;
using Brigthlocal.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Examples
{
    class RankingsExamples
    {
        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'search' or 1, 'bulk search' or 2. Plese enter a command. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command.Trim())
                {
                    case "search":
                    case "1":
                        Search(apiKey, apiSecret);
                        break;
                    case "bulk search":
                    case "2":
                        BulkSearch(apiKey, apiSecret);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
            } while (true);
        }

        private static void Search(string apiKey, string apiSecret)
        {

            List<object> searches = new List<object>
            {
                new
                {
                    search_engine = "google",
                    country = "USA",
                    google_location = "New York, NY",
                    search_term = "restaurant new york",
                    urls = JsonConvert.SerializeObject(new List<string> { "le-bernardin.com" }),
                    business_names = JsonConvert.SerializeObject(new List<string>() { "Le Bernardin" })
                },
                new
                {
                    search_engine = "google",
                    country = "USA",
                    google_location = "New York, NY",
                    search_term = "restaurant manhattan",
                    urls = JsonConvert.SerializeObject( new List<string> { "le-bernardin.com" }),
                    business_names = JsonConvert.SerializeObject( new List<string>() { "Le Bernardin" })
                },
                new
                {
                    search_engine = "google",
                    country = "USA",
                    google_location = "New York, NY",
                    search_term = "restaurant 10019",
                    urls =JsonConvert.SerializeObject( new List<string> { "le-bernardin.com" }),
                    business_names = JsonConvert.SerializeObject(new List<string>() { "Le Bernardin" })
                }
            };

            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.CreateBatch();
            Console.WriteLine("Created batch ID {0}", batch.GetId());
            foreach (object search in searches)
            {
                try
                {
                    Parameters parameters = new Parameters(search);
                    // Add jobs to batch
                    dynamic jobResponse = batch.AddJob("/v4/rankings/search", parameters);
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

        private static void BulkSearch(string apiKey, string apiSecret)
        {
            List<string> searches = new List<string>
            {
                "restaurant new york",
                "restaurant manhattan",
                "restaurant 10019"
            };

            Api api = new Api(apiKey, apiSecret);
            Batch batch = api.CreateBatch();
            Console.WriteLine("Created batch ID {0}", batch.GetId());
            Parameters parameters = new Parameters(
                new
                {
                    search_engine = "google",
                    country = "USA",
                    google_location = "New York, NY",
                    search_terms = JsonConvert.SerializeObject(searches),
                    urls = JsonConvert.SerializeObject(new List<string> { "le-bernardin.com" }),
                    business_names = JsonConvert.SerializeObject(new List<string> { "Le Bernardin" })
                });
            try
            {
                // Add jobs to batch
                dynamic jobResponse = batch.AddJob("/v4/rankings/bulk-search", parameters);
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

    }
}