using Brightlocal;
using Brigthlocal;
using System;

namespace Examples
{
    class ClientsExample
    {
        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'add client' or 1, 'update client' or 2, 'delete client' or 3, 'get client' or 4, 'search clients' or 5. Plese enter a command. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command.Trim())
                {
                    case "add client":
                    case "1":
                        AddClient(apiKey, apiSecret);
                        break;  
                    case "update client":
                    case "2":
                        UpdateClient(apiKey, apiSecret);
                        break; 
                    case "delete client":
                    case "3":
                        DeleteClient(apiKey, apiSecret);
                        break;  
                    case "get client":
                    case "4":
                        GetClient(apiKey, apiSecret);
                        break;  
                    case "search clients":
                    case "5":
                        SearchClients(apiKey, apiSecret);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
            } while (true);
        }

        private static void AddClient(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
                {
                    { "name", "Le Bernardin" },
                    { "company-url", "le-bernardin.com" }
                };
            Response response = api.Post("/v1/clients-and-locations/clients", parameters);
            Console.WriteLine(response.GetContent());
        }

        private static void UpdateClient(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            string clientId = "197905";
            Parameters parameters = new Parameters
                {
                    { "name", "Le Bernardin" },
                    { "company-url", "le-bernardin.com" }
                };
            Response response = api.Put("/v1/clients-and-locations/clients/" + clientId, parameters);
            Console.WriteLine(response.GetContent());
        }
        
        private static void DeleteClient(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            string clientId = "197904";
            Parameters parameters = new Parameters { };
            Response response = api.Delete("/v1/clients-and-locations/clients/" + clientId, parameters);
            if (response.IsSuccess())
            {
                Console.WriteLine("Successfully deleted client");
            }
            else
            {
                Console.WriteLine(response.GetContent());
            }
        }  

        private static void GetClient(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            string clientId = "197908";
            Response response = api.Get("/v1/clients-and-locations/clients/" + clientId);
            Console.WriteLine(response.GetContent());
        }
        
        private static void SearchClients(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters {
                   { "q", "BrightLocal" }
            };
            Response response = api.Get("/v1/clients-and-locations/clients/search", parameters);
            Console.WriteLine(response.GetContent());
        }
    }
}
