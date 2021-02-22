using Brightlocal;
using Brigthlocal;
using Brigthlocal.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Examples
{
    class CitationTrackerExamples
    {

        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine(@"For CT reports now you can: 'add report' or 1, 'update report' or 2  'get report' or 3, 'delete report' or 4, 'get reports or 5," +
                    "'get all reports' or 6, 'run report' or 7, 'get report result' or 8, exit or 0");


                string command = Console.ReadLine();
                switch (command)
                {
                    case "add report":
                    case "1":
                        AddReport(apiKey, apiSecret);
                        break;
                    case "update report":
                    case "2":
                        UpdateReport(apiKey, apiSecret);
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
                    case "get all reports":
                    case "6":
                        int locationId = Program.GetIntegerValue("Enter location id from what you want to CT reports");
                        GetAllReports(apiKey, apiSecret, locationId);
                        break;
                    case "run report":
                    case "7":
                        int reportIdRun = Program.GetIntegerValue("Enter report id that you want to run");
                        RunReport(apiKey, apiSecret, reportIdRun);
                        break;                    
                    case "get report result":
                    case "8":
                        int reportIdGet = Program.GetIntegerValue("Enter report id that you want to get result");
                        GetReportResult(apiKey, apiSecret, reportIdGet);
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
                { "location-id", 1707816 },
                { "report-name", "Le Bernardin" },
                { "business-name", "Le Bernardin" },
                { "phone", "+1 212-554-1515" },
                { "address-1", "155 West 51st Street" },
                { "business-location", "New York, NY" },
                { "postcode", "10019" },
                { "website", "le-bernardin.com" },
                { "business-type", "Restaurant" },
                { "state-code", "NY" },
                { "primary-location", "10019" },
            };

            dynamic response = api.Post("v2/ct/add", parameters).GetContent();
            Console.WriteLine(response);
        }

        private static void UpdateReport(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
            {
                { "location-id", 971017 },
                { "report-id", 998579 },
                { "report-name", "Le Bernardin updated" },
                { "business-name", "Le Bernardin" },
                { "phone", "+1 212-554-1515" },
                { "address-1", "155 West 51st Street" },
                { "business-location", "New York, NY" },
                { "postcode", "10019" },
                { "website", "le-bernardin.com" },
                { "business-type", "Restaurant" },
                { "state-code", "NY" },
                { "primary-location", "10019" },
            };

            dynamic response = api.Post("v2/ct/update", parameters).GetContent();
            Console.WriteLine(response);
        }

        private static void DeleteReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response status = api.Post(
                "v2/ct/delete",
                new Parameters { { "report-id", reportId } });
            Console.WriteLine(status.GetContent());
        }

        private static void GetReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response report = api.Get("v2/ct/get", new Parameters { { "report-id", reportId } });
            Console.WriteLine(report.GetContent());
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

        private static void GetAllReports(string apiKey, string apiSecret, int locationId)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
            {
                { "location-id", locationId }
            };
            Response reports = api.Get("v2/ct/get-all", parameters);
            Console.WriteLine(reports.GetContent());
        }

        private static void RunReport(string apiKey, string apiSecret, int reportId)
        {

            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
            {
                { "report-id", reportId }
            };
            Response response = api.Post("v2/ct/run", parameters);
            Console.WriteLine(response.GetContent());
        }

        private static void GetReportResult(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Get("v2/ct/get-results", new Parameters
            {
                { "report-id", reportId }
            });
            Console.WriteLine(response.GetContent());
        }
    }
}
