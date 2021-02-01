
using System;

namespace Examples
{
    class Program
    {

        static void Main(string[] args)
        {
            string apiKey = "1a3c2fa6735f089a2a1dd4fa11067807383bd08c";
            string apiSecret = "5a0ae446a98a1";


            Console.WriteLine("Welcome to Brightlocal testing tool.");
            do
            {
                try
                {
                    Console.WriteLine("Please enter a command. Supported command is 'batch', 'local directories'");
                    Console.WriteLine("Type 'exit' to go close application");
                    string command = Console.ReadLine();
                    switch (command.Trim())
                    {
                        case "batch":
                            BatchExample.Process(apiKey, apiSecret);
                            break;
                        case "local directories":
                            BatchExample.Process(apiKey, apiSecret);
                            break;
                        case "exit":
                            Environment.Exit(1);
                            break;
                        default:
                            Console.WriteLine("Unsupported command '{0}'", command);
                            break;
                    }
                }
                catch (Brigthlocal.Exceptions.GeneralException exception)
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
                bool success = Int32.TryParse(input, out int number);
                if (success)
                {
                    return number;
                }
                Console.WriteLine("You should enter integer value here.");
            } while (true);
        }
    }
}
