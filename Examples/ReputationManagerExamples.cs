using Brightlocal;
using System;
using System.Collections.Generic;

namespace Examples
{
    class ReputationManagerExamples
    {     
        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine(@"Now you can: 'add report' or 1, 'update report' or 2  'get report' or 3, 'delete report' or 4, 'get reports or 5," +
                    "'search reports' or 6, 'get reviews' or 7, 'get reviews count' or 8, 'get growth' or 9, 'get directories' or 10," +
                    "'get directory stats' or 11, 'get star counts' or 12, exit or 0");


                string command = Console.ReadLine();
                switch (command)
                {
                    case "add report":
                    case "1":
                        AddReport(apiKey, apiSecret);
                        break;
                    case "update report":
                    case "2":
                        int reportIdUpdate = Program.GetIntegerValue("Enter report id that you want to update");
                        UpdateReport(apiKey, apiSecret, reportIdUpdate);
                        break;
                    case "get report":
                    case "3":
                        int reportIdToGet = Program.GetIntegerValue("Enter report id that you want to get");
                        GetReport(apiKey, apiSecret, reportIdToGet);
                        break;
                    case "delete report":
                    case "4":
                        int reportId = Program.GetIntegerValue("Enter report id that you want to delete");
                        DeleteReport(apiKey, apiSecret, reportId);
                        break;
                    case "get reports":
                    case "5":
                        GetReports(apiKey, apiSecret);
                        break;
                    case "search reports":
                    case "6":
                        SearchReports(apiKey, apiSecret);
                        break;
                    case "get reviews":
                    case "7":
                        int reportIdReviews = Program.GetIntegerValue("Enter report id that you want to get reviews");
                        GetReviews(apiKey, apiSecret, reportIdReviews);
                        break;
                    case "get reviews count":
                    case "8":
                        int reportIdReviewsCount = Program.GetIntegerValue("Enter report id that you want to get reviews count");
                        GetReviewsCount(apiKey, apiSecret, reportIdReviewsCount);
                        break;
                    case "get growth":
                    case "9":
                        int reportIdgrows = Program.GetIntegerValue("Enter report id that you want to get growth");
                        GetGrowth(apiKey, apiSecret, reportIdgrows);
                        break;
                    case "get directories":
                    case "10":
                        int reportIdDirectories = Program.GetIntegerValue("Enter report id that you want to get directories");
                        GetDirectories(apiKey, apiSecret, reportIdDirectories);
                        break;
                    case "get directory stats":
                    case "11":
                        int reportIdStats = Program.GetIntegerValue("Enter report id that you want to get directory stats");
                        GetDirectoryStats(apiKey, apiSecret, reportIdStats);
                        break;
                    case "get star counts":
                    case "12":
                        int reportIdStars = Program.GetIntegerValue("Enter report id that you want to get star counts");
                        GetStarCounts(apiKey, apiSecret, reportIdStars);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
            } while (true);
        }

        private static void AddReport(string apiKey, string apiSecret)
        {
             Dictionary<string, object> directories = new Dictionary<string, object>()
            {
                {
                    "google", new {
                        url = "https://www.google.com/search?gl=us&hl=en&sxsrf=ALeKk00JY-ZkNTiMYNy5HejLxQSpbHyYGQ%3A1612777965993&ei=7QkhYKSBPPDJrgSu47SwCw&q=mcdonalds&gs_ssp=eJzj4tTP1TcwT68sMFdgNGB0YPDizE1Oyc9LzEkpBgBbWwdn&oq=MacDonalds&gs_lcp=CgZwc3ktYWIQARgAMg0ILhDHARCjAhBDEJMCMgcIABAKEMsBMgcIABAKEMsBMgcIABAKEMsBMgcIABDJAxAKMgUIABCSAzIHCAAQChDLATICCAAyBwgAEAoQywEyBAgAEAo6BwgAEEcQsAM6BAgjECc6CgguEMcBEK8BEEM6CAguEMcBEKMCOgcIIxDqAhAnOgQIABBDOgQILhBDOgoILhDHARCjAhBDOgIILjoFCAAQkQI6CAguEMcBEK8BOgUIABDLAToFCC4QywFQuCdY4E9gmWFoA3ACeACAAecBiAHWFJIBBjAuMTAuNJgBAKABAaoBB2d3cy13aXqwAQrIAQjAAQE&sclient=psy-ab",
                        include = true
                    }
                }
            }; 
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
            {
                { "location-id", 738649 },
                { "report-name", "Le Bernardin" },
                { "business-name", "Le Bernardin" },
                { "contact-telephone", "+1 212-554-1515" },
                { "address1", "155 West 51st Street" },
                { "city", "New York" },
                { "postcode", "10019" },
                { "country", "USA" },
                { "directories", directories }
            };

           dynamic response = api.Post("v4/rf/add", parameters).GetContent();
            Console.WriteLine(response);            
        }

        private static void UpdateReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            dynamic directories = new
            {
                google = new
                {
                    url = "https://www.google.com/search?gl=us&hl=en&sxsrf=ALeKk00JY-ZkNTiMYNy5HejLxQSpbHyYGQ%3A1612777965993&ei=7QkhYKSBPPDJrgSu47SwCw&q=mcdonalds&gs_ssp=eJzj4tTP1TcwT68sMFdgNGB0YPDizE1Oyc9LzEkpBgBbWwdn&oq=MacDonalds&gs_lcp=CgZwc3ktYWIQARgAMg0ILhDHARCjAhBDEJMCMgcIABAKEMsBMgcIABAKEMsBMgcIABAKEMsBMgcIABDJAxAKMgUIABCSAzIHCAAQChDLATICCAAyBwgAEAoQywEyBAgAEAo6BwgAEEcQsAM6BAgjECc6CgguEMcBEK8BEEM6CAguEMcBEKMCOgcIIxDqAhAnOgQIABBDOgQILhBDOgoILhDHARCjAhBDOgIILjoFCAAQkQI6CAguEMcBEK8BOgUIABDLAToFCC4QywFQuCdY4E9gmWFoA3ACeACAAecBiAHWFJIBBjAuMTAuNJgBAKABAaoBB2d3cy13aXqwAQrIAQjAAQE&sclient=psy-ab",
                    include = true
                },
                yelp = new
                {
                    url = "",
                    include = false
                }
            };

            Parameters parameters = new Parameters
            {
                { "location-id", 738649 },
                { "report-name", "Le Bernardin updated" },
                { "directories", directories }
            };

            dynamic reviews = api.Put("v4/rf/" + reportId, parameters).GetContent();
            Console.WriteLine(reviews);
        }

        private static void DeleteReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            dynamic status = api.Delete("v4/rf/" + reportId).GetContent();
            Console.WriteLine(status);
        }

        private static void GetReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            dynamic report = api.Get("v4/rf/" + reportId).GetContent();
            Console.WriteLine(report);
        }

        private static void GetReports(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
            {
                { "location-id", 393135 }
            };
            dynamic report = api.Get("v4/rf", parameters).GetContent();
            Console.WriteLine(report);
        }

        private static void SearchReports(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
            {
                { "q", "Le Bernardin" }
            };
            dynamic report = api.Get("v4/rf/search", parameters).GetContent();
            Console.WriteLine(report);
        }

        private static void GetReviews(string apiKey, string apiSecret, int reportId)
        {

            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
            {
                { "limit", 100 }
            };
            dynamic reviews = api.Get("v4/rf/" + reportId + "/reviews", parameters).GetContent();
            Console.WriteLine(reviews);
        }

        private static void GetReviewsCount(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            dynamic response = api.Get("v4/rf/" + reportId + "/reviews/count").GetContent();
            Console.WriteLine(response);
        }
        private static void GetGrowth(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            dynamic grows = api.Get("v4/rf/" + reportId + "/reviews/growth").GetContent();
            Console.WriteLine(grows);
        }

        private static void GetDirectories(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            dynamic directories = api.Get("v4/rf/" + reportId + "/directories").GetContent();
            Console.WriteLine(directories);
        }

        private static void GetDirectoryStats(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            dynamic response = api.Get("v4/rf/" + reportId + "/directories/stats").GetContent();
            Console.WriteLine(response);
        } 
        
        private static void GetStarCounts(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            dynamic response = api.Get("v4/rf/" + reportId + "/stars/count").GetContent();
            Console.WriteLine(response);
        }
    }
}
