using Brightlocal;
using Brigthlocal.Exceptions;
using Newtonsoft.Json;
using System;

namespace Examples
{
    class ReputationManagerExamples
    {
        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'get reviews' or 7. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command.Trim())
                {
                    case "get reviews":
                    case "7":
                        GetReviews(apiKey, apiSecret, 344406);
                        break;
                    case "fetch profile url by telephone":
                    case "2":
                        break;
                    case "fetch profile details":
                    case "3":
                        break;
                    case "fetch profile details by business data":
                    case "4":
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
            } while (true);
        }

        private static void GetReviews(string apiKey, string apiSecret, int reportId)
        {

            Api api = new Api(apiKey, apiSecret);
            var parameters = new Parameters
            {
                { "limit", 100 }
            };
            dynamic reviews = api.Get("v4/rf/" + reportId + "/reviews", parameters).GetContent();
            Console.WriteLine(reviews);
        }
    }
}
