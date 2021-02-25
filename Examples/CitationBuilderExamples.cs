using Brightlocal;
using System;
using System.Collections.Generic;

namespace Examples
{
    class CitationBuilderExamples
    {

        public static void Process(string apiKey, string apiSecret)
        {
            do
            {
                Console.WriteLine(@"For CB reports now you can: 'create campaing' or 1, 'upload image' or 2, 'exit' or 0");


                string command = Console.ReadLine();
                switch (command)
                {
                    case "create campaing":
                    case "1":
                        CreateCampaing(apiKey, apiSecret);
                        break;
                    case "upload image":
                    case "2":
                        UploadImage(apiKey, apiSecret);
                        break;                   
                    case "exit":
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Unsupported command");
                        break;
                }
            } while (true);
        }

        private static void CreateCampaing(string apiKey, string apiSecret)
        {
            string briefDescription = "Born in Paris in 1972 by sibling duo Maguy and Gilbert Le Coze, Le Bernardin only served fish: " +
                "Fresh, simple and prepared with respect.";
            string fullDescription = "The restaurant has held three stars from the Michelin Guide since its 2005 New York launch" +
                " and currently ranks 24 on the World’s 50 Best Restaurants list. The New York Zagat Guide has recognized Le Bernardin" +
                " as top rated in the category of “Best Food” for the last nine consecutive years, and in 2015 was rated by the guide as " +
                "New York City’s top restaurant for food and service.  Le Bernardin has earned seven James Beard Awards since 1998 " +
                "including “Outstanding Restaurant of the Year”";
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
                { "location_id"                , 1 },
                { "campaign_name"              , "Le Bernardin Citation Builder"},
                { "business_name"              , "Le Bernardin"},
                { "website_address"            , "le-bernardin.com"},
                { "campaign_country"           , "USA"},
                { "campaign_city"              , "New York"},
                { "campaign_state"             , "NY"},
                { "business_category_id"       , 605},
                { "business_categories"        , new List<string>() { "restaurant", "cafe" } },
                { "address1"                   , "155 West 51st Street"},
                { "address2"                   , ""},
                { "city"                       , "New York"},
                { "region"                     , "New York, USA"},
                { "postcode"                   , "10019"},
                { "contact_name"               , "Bloggs"},
                { "contact_firstname"          , "Joe"},
                { "contact_telephone"          , "+1 212-554-1515"},
                { "contact_email"              , "joe.bloggs@test.com"},
                { "brief_description"          , briefDescription},
                { "full_description"           , fullDescription},
                { "employees_number"           , 35},
                { "formation_date"             , "11-2008" },
                { "white_label_profile_id"     , 1 },
                { "opening_hours"              , opening_hours }
            };

            dynamic response = api.Post("v4/cb/create", parameters).GetContent();
            Console.WriteLine(response);
        }

        private static void UploadImage(string apiKey, string apiSecret)
        {
            //FileStream fstream = new FileStream($"../../../Brightlocal/logo.jpg", FileMode.Open);
            Api api = new Api(apiKey, apiSecret);
            /*Parameters parameters = new Parameters
            {
                { "file", fstream. },
            };
            fstream.Close();*/
            Response response = api.PostImage("v2/cb/upload/468460/logo", @"../../../Brightlocal/logo.jpg");
            Console.WriteLine(response.GetResponseCode());
            Console.WriteLine(response.IsSuccess());
            Console.WriteLine(response.GetContent());
        }      
    }
}
