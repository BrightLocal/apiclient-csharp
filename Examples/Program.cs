
using System;

namespace Examples
{
    class Program
    {

        static void Main()
        {
            string apiKey = "1a3c2fa6735f089a2a1dd4fa11067807383bd08c";
            string apiSecret = "5a0ae446a98a1";

            Console.WriteLine("Welcome to Brightlocal testing tool.");
            do
            {
                try
                {
                    Console.WriteLine("Please enter a command. Supported command is 'batch' or 1, 'local directories' or 2," +
                        " 'reputation manager' or 3, 'reviews' or 4, 'rankings' or 5, 'offsite seo' or 6," +
                        " 'clients' or 7, 'rank checker' or 8, 'locations' or 9, 'local search audit' or 10, 'citation tracker' or 11, 'citation builder' or 12");
                    Console.WriteLine("Type 'exit' to go close application");
                    string command = Console.ReadLine();
                    switch (command.Trim())
                    {
                        case "batch":
                        case "1":
                            BatchExample.Process(apiKey, apiSecret);
                            break;
                        case "local directories":
                        case "2":
                            LocalDirectoriesExamples.Process(apiKey, apiSecret);
                            break;
                        case "reputation manager":
                        case "3":
                            ReputationManagerExamples.Process(apiKey, apiSecret);
                            break;
                        case "reviews":
                        case "4":
                            ReviewsExamples.Process(apiKey, apiSecret);
                            break;
                        case "rankings":
                        case "5":
                            RankingsExamples.Process(apiKey, apiSecret);
                            break;
                        case "offsite seo":
                        case "6":
                            OffsiteSeoExample.Process(apiKey, apiSecret);
                            break;
                        case "clients":
                        case "7":
                            ClientsExample.Process(apiKey, apiSecret);
                            break;
                        case "rank checker":
                        case "8":
                            RankCheckerExamples.Process(apiKey, apiSecret);
                            break;
                        case "locations":
                        case "9":
                            LocationsExample.Process(apiKey, apiSecret);
                            break;
                        case "local search audit":
                        case "10":
                            LocalSearchAuditExample.Process(apiKey, apiSecret);
                            break;
                        case "citation tracker":
                        case "11":
                            CitationTrackerExamples.Process(apiKey, apiSecret);
                            break;
                        case "citation builder":
                        case "12":
                            CitationBuilderExamples.Process(apiKey, apiSecret);
                            break;
                        case "exit":
                            Environment.Exit(1);
                            break;
                        default:
                            Console.WriteLine("Unsupported command '{0}'", command);
                            break;
                    }
                }
                catch (Brightlocal.Exceptions.GeneralException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.ReadLine();
                }
            } while (true);
        }

        public static int GetIntegerValue(string message)
        {
            Console.WriteLine(message);
            do
            {
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    Environment.Exit(1);
                }
                bool success = int.TryParse(input, out int number);
                if (success)
                {
                    return number;
                }
                Console.WriteLine("You should enter integer value here.");
            } while (true);
        }
    }
}
