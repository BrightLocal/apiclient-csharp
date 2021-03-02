using Brightlocal;
using System;

namespace Examples
{
    class BusinessCategoriesExample
    {

        public static void Process(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            string country = "USA";
            Response response = api.Get("/v1/business-categories/" + country);
            Console.WriteLine(response.GetContent());
        }

    }
}
