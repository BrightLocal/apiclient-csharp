using Brightlocal;
using Brightlocal;
using System;
using System.Collections.Generic;

namespace Examples
{
    class LocalSearchAuditExample
    {
        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'add report' or 1, 'update report' or 2, 'delete report' or 3, 'get report' or 4, 'run report' or 5, 'search' or 6. Plese enter a command. Type 'exit' to go to main menu.");
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
                        int reportIdDelete = Program.GetIntegerValue("Enter report id that you want to delete");
                        DeleteReport(apiKey, apiSecret, reportIdDelete);
                        break;
                    case "get report":
                    case "4":
                        int reportIdGet = Program.GetIntegerValue("Enter report id that you want to get");
                        GetReport(apiKey, apiSecret, reportIdGet);
                        break;
                    case "run report":
                    case "5":
                        int reportIdRun = Program.GetIntegerValue("Enter report id that you want to run");
                        RunReport(apiKey, apiSecret, reportIdRun);
                        break;
                    case "search":
                    case "6":
                        SearchReport(apiKey, apiSecret);
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
                    { "report-name", "Sample Local Search Audit Report" },
                    { "location-id", 1707816 },
                    { "business-names", new List<string> { "le-bernardin.com" } },
                    { "search-terms", new List<string> { "restaurant manhattan", "cafe new york" } },
                    { "website-address", "le-bernardin.com" },
                    { "country", "USA"},
                    { "address1", "155 Weest 51st Street"},
                    { "region", "NY"},
                    { "city", "New York"},
                    { "state-code", "10019"},
                    { "telephone", "+1 212-554-1515"},
                    { "primary-business-location", "NY, New York"},
                    { "exclude-sections", new List<string> { "social-channels" } },
                };
            Response response = api.Post("v4/lscu", parameters);
            Console.WriteLine(response.GetContent());
        }

        private static void UpdateReport(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
                {
                    { "report-id", 382643 },
                    { "report-name", "Sample LSCU Updated" },
                    { "location-id", 1345471 },
                    { "business-names", new List<string> { "le-bernardin.com" } },
                    { "search-terms", new List<string> { "restaurant manhattan", "cafe new york" } },
                    { "website-address", "le-bernardin.com" },
                    { "country", "USA"},
                    { "address1", "155 West 51st Street"},
                    { "region", "NY"},
                    { "city", "New York"},
                    { "telephone", "+2 212-554-1515"},
                    { "primary-business-location", "NY, New York"},
                    { "exclude-sections", new List<string> { "social-channels", "local-listings-and-reviews" } },
                };
            Response response = api.Put("v4/lscu", parameters);
            Console.WriteLine(response.GetContent());

        }

        private static void DeleteReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Delete("v4/lscu", new Parameters { ["report-id"] = reportId });
            if (response.IsSuccess())
            {
                Console.WriteLine("Location successfully deleted.");
            }
            else
            {
                Console.WriteLine(response.GetContent());
            }
        }

        private static void RunReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Put("v4/lscu/run", new Parameters { ["report-id"] = reportId });
            Console.WriteLine(response.GetContent());
        }

        private static void GetReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Get("v4/lscu", new Parameters { ["report-id"] = reportId });
            Console.WriteLine(response.GetContent());
        }

        private static void SearchReport(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters {
                   { "q", "BrightLocal" }
            };
            Response response = api.Get("v4/lscu/search", parameters);
            Console.WriteLine(response.GetContent());
        }
    }
}
