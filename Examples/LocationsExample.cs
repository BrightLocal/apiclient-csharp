using Brightlocal;
using System;
using System.Collections.Generic;

namespace Examples
{
    class LocationsExample
    {
        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine("Now you can 'add location' or 1, 'update location' or 2, 'delete location' or 3, 'get location' or 4, 'search locations' or 5. Plese enter a command. Type 'exit' to go to main menu.");
                string command = Console.ReadLine();
                switch (command.Trim())
                {
                    case "add location":
                    case "1":
                        AddLocation(apiKey, apiSecret);
                        break;
                    case "update location":
                    case "2":
                        int locationId = Program.GetIntegerValue("Enter location id that you want to update");
                        UpdateLocation(apiKey, apiSecret, locationId);
                        break;
                    case "delete location":
                    case "3":
                        int locationIdDelete = Program.GetIntegerValue("Enter location id that you want to delete");
                        DeleteLocation(apiKey, apiSecret, locationIdDelete);
                        break;
                    case "get locations":
                    case "4":
                        int locationIdGet = Program.GetIntegerValue("Enter location id that you want to get");
                        GetLocation(apiKey, apiSecret, locationIdGet);
                        break;
                    case "search locations":
                    case "5":
                        SearchLocations(apiKey, apiSecret);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
            } while (true);
        }
        private static void AddLocation(string apiKey, string apiSecret)
        {
            dynamic opening_hours = new
            {
                regular = new Dictionary<string, object>()
                    {
                        { "apply-to-all" , false },
                        {  "mon" , new {
                                        status = "open",
                                        hours = new List<object>{
                                            new {
                                                start = "10:00",
                                                end = "18:00"
                                            }
                                        }
                                  }
                        },
                        {  "tue" , new {
                                        status = "split",
                                        hours = new List<object>{
                                            new {
                                                start = "10:00",
                                                end = "12:00"
                                            },
                                            new {
                                                start = "13:00",
                                                end = "18:00"
                                            },
                                        }
                                  }
                        },
                        {  "wed" , new {
                                        status = "24hrs",
                                  }
                        },
                        {  "thu" , new {
                                        status = "open",
                                        hours = new List<object>{
                                            new {
                                                start = "10:00",
                                                end = "18:00"
                                            }
                                        }
                                  }
                        },
                        {  "fri" , new {
                                        status = "open",
                                        hours = new List<object>{
                                            new {
                                                start = "10:00",
                                                end = "18:00"
                                            }
                                        }
                                  }
                        },
                        {  "sat" , new {
                                        status = "closed",
                                  }
                        },
                        {  "sun" , new {
                                        status = "closed",
                                  }
                        },
                    },
                special = new List<object>()
                {
                    new {
                        date = "2021-01-27",
                        status = "closed",
                    }
                }
            };

            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
                {
                    { "name", "Le Bernardin" },
                    { "location-reference", "LE-BERNARDIN-1009969" },
                    { "url", "le-bernardin.com" },
                    { "business-category-id", "605" },
                    { "country", "USA"},
                    { "address1", "155 Weest 51st Street"},
                    { "region", "NY"},
                    { "city", "New York"},
                    { "postcode", "10019"},
                    { "telephone", "+1 212-554-1515"},
                    { "opening-hours", opening_hours }
                };
            Response response = api.Post("v2/clients-and-locations/locations/", parameters);
            Console.WriteLine(response.GetContent());

        }

        private static void UpdateLocation(string apiKey, string apiSecret, int locationId)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters
                {
                    { "name", "Le Bernardin" },
                    { "location-reference", "LE-BERNARDIN-100999" },
                    { "url", "le-bernardin.com" },
                    { "business-category-id", "605" },
                    { "country", "USA"},
                    { "address1", "155 Weest 51st Street"},
                    { "region", "NY"},
                    { "city", "New York"},
                    { "postcode", "10019"},
                    { "telephone", "+1 212-554-1515"},
                    { "opening-hours",  new
                        { special = new List<object>()
                            {
                                new {
                                    date = "2021-12-31",
                                    status = "closed",
                                }
                            }
                        }
                    }
                };
            Response response = api.Put("/v2/clients-and-locations/locations/" + locationId, parameters);
            Console.WriteLine(response.GetContent());

        }

        private static void DeleteLocation(string apiKey, string apiSecret, int locationId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Delete("/v2/clients-and-locations/locations/" + locationId);
            if (response.IsSuccess())
            {
                Console.WriteLine("Location successfully deleted.");
            }
            else
            {
                Console.WriteLine(response.GetContent());
            }
        }

        private static void GetLocation(string apiKey, string apiSecret, int locatonId)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Get("/v2/clients-and-locations/locations/" + locatonId);
            Console.WriteLine(response.GetContent());
        }

        private static void SearchLocations(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Parameters parameters = new Parameters {
                   { "q", "BrightLocal" }
            };
            Response response = api.Get("/v2/clients-and-locations/locations/search", parameters);
            Console.WriteLine(response.GetContent());
        }
    }
}
