using Brightlocal;
using System;

namespace Examples
{
    class GetListSupportedDirectoriesExample
    {

        public static void Process(string apiKey, string apiSecret)
        {
            Api api = new Api(apiKey, apiSecret);
            Response response = api.Get("v1/directories/all" );
            Console.WriteLine(response.GetContent());
        }

    }
}
