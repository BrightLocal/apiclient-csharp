using Brightlocal;
using Brigthlocal;
using System;

namespace Examples
{
    class RankCheckerExamples
    {

        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'add report' or 1, 'update report' or 2, 'delete report' or 3, 'get all reports' or 4, 'get report' or 5, 'run report' or 6, 'get report history' or 7, 'get report result'  or 8. Plese enter a command. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command.Trim())
                {
                    case "add report":
                    case "1":
                        AddReport(apiKey, apiSecret);
                        break;
                    case "update report":
                    case "2":
                        UpdateReport(apiKey, apiSecret);
                        break;
                    case "delete report":
                    case "3":
                        DeleteReport(apiKey, apiSecret);
                        break;
                    case "get all reports":
                    case "4":
                        GetAllReports(apiKey, apiSecret);
                        break;
                    case "get report":
                    case "5":
                        GetReport(apiKey, apiSecret);
                        break;
                    case "run report":
                    case "6":
                        RunReport(apiKey, apiSecret);
                        break;
                    case "get report history":
                    case "7":
                        GetReportHistory(apiKey, apiSecret);
                        break;
                    case "get report result":
                    case "8":
                        GetReportResults(apiKey, apiSecret);
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
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
                {
                    { "location-id", 1},
                    { "name", "Le Bernardin" },
                    { "schedule", "Adhoc" },
                    { "search-terms", "Restaurant\nfood+nyc\ndelivery+midtown+manhattan" },
                    { "website-addresses", "['le-bernardin.com','le-bernardin2.com']" },
                    { "search-engines", "google,google-mobile,google-local,bing,bing-local" }

        };
            Response response = api.Post("/v2/lsrc/add", parameters);
            Console.WriteLine(response.GetContent());

        }
        private static void UpdateReport(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
                {
                    { "location-id", 1},
                    { "campaign-id", 9907},
                    { "name", "Le Bernardin" },
                    { "schedule", "Adhoc" },
                    { "search-terms", "Restaurant\nfood+nyc\ndelivery+midtown+manhattan" },
                    { "website-addresses", "['le-bernardin.com','le-bernardin2.com']" },
                    { "search-engines", "google,google-mobile,google-local,bing,bing-local" }

        };
            Response response = api.Post("/v2/lsrc/update", parameters);
            Console.WriteLine(response.GetContent());

        }
        private static void DeleteReport(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters {
                   { "campaign-id", 9907 }
            };
            Response response = api.Post("/v2/lsrc/delete", parameters);
            Console.WriteLine("Successfully deleted report");
        }
        private static void GetAllReports(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters { };
            Response response = api.Get("v2/lsrc/get-all", parameters);
            Console.WriteLine(response.GetContent());
        }
        private static void GetReport(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters {
                   { "campaign-id", 50 }
            };
            Response response = api.Get("/v2/lsrc/get", parameters);
            Console.WriteLine(response.GetContent());
        }
        private static void RunReport(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters {
                   { "campaign-id", 50 }
            };
            Response response = api.Post("/v2/lsrc/run", parameters);
            Console.WriteLine(response.GetContent());
        }
        private static void GetReportHistory(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters {
                   { "campaign-id", 50 }
            };
            Response response = api.Get("/v2/lsrc/history/get", parameters);
            Console.WriteLine(response.GetContent());
        }
        private static void GetReportResults(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters {
                   { "campaign-id", 9636 }
            };
            Response response = api.Get("/v2/lsrc/results/get", parameters);
            Console.WriteLine(response.GetContent());
        }

    }
}
