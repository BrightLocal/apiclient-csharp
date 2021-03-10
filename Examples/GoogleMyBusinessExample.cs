using Brightlocal;
using System;
using System.Collections.Generic;

namespace Examples
{
    class GoogleMyBusinessExample
    {
        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'add report' or 1, 'update report' or 2, 'get report' or 3, 'delete report' or 4,'get all reports' or 5, 'run report' or 6, 'get report results' or 7. Plese enter a command. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command.Trim())
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
                        int reportIdGet = Program.GetIntegerValue("Enter report id that you want to get");
                        GetReport(apiKey, apiSecret, reportIdGet);
                        break;
                    case "delete report":
                    case "4":
                        int reportIdDelete = Program.GetIntegerValue("Enter report id that you want to delete");
                        DeleteReport(apiKey, apiSecret, reportIdDelete);
                        break;
                    case "get all reports":
                    case "5":
                        GetAllReports(apiKey, apiSecret);
                        break;
                    case "run report":
                    case "6":
                        int reportIdRun = Program.GetIntegerValue("Enter report id that you want to run");
                        RunReport(apiKey, apiSecret, reportIdRun);
                        break;
                    case "get report results":
                    case "7":
                        int reportIdGetReports = Program.GetIntegerValue("Enter report id that you want to get results");
                        GetReportResults(apiKey, apiSecret, reportIdGetReports);
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
                    { "location_id", 1973217 },
                    { "report_name", "Le Bernardin" },
                    { "business_names", "Le Bernardin" },
                    { "schedule", "Adhoc" },
                    { "day_of_month", "2" },
                    { "report_type", "with" },
                    { "address1", "155 West 51st Street" },
                    { "address2", "" },
                    { "city", "New York" },
                    { "state_code", "NY" },
                    { "google_location", "New York, NY" },
                    { "postcode", "10019" },
                    { "phone_number", "+1 212-554-1515" },
                    { "country", "USA" },
                    { "search_terms", new List<string> { "restaurant manhattan", "cafe new york" } },
                };
            Response response = api.Post("/v4/gpw/add", parameters);
            Console.WriteLine(response.GetContent());
        }

        private static void UpdateReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
                {

                    { "location-id", 1867606 },
                    { "business-names", "Le Bernardin" },
                    { "contact-telephone", "+1 212-554-1515" },
            
                };
            Response response = api.Put("/v4/gpw/" + reportId, parameters);
            Console.WriteLine(response.GetContent());

        }
        private static void GetReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Get("v4/gpw/" + reportId);
            Console.WriteLine(response.GetContent());
        }

        private static void DeleteReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Delete("/v4/gpw/" + reportId);
            if (response.IsSuccess())
            {
                Console.WriteLine("Successfully deleted report.");
            }
            else
            {
                Console.WriteLine(response.GetContent());
            }
        }

        private static void GetAllReports(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Get("/v4/gpw/");
            Console.WriteLine(response.GetContent());
        }
        private static void RunReport(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Put("/v4/gpw/run", new Parameters { ["report-id"] = reportId });
            Console.WriteLine(response.GetContent());
        }

        private static void GetReportResults(string apiKey, string apiSecret, int reportId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Get("/v4/gpw/" + reportId + "/results");
            Console.WriteLine(response.GetContent());
        }

    }
}
