
using Brightlocal;
using System;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Parametrs parametrs = new Parametrs();
            Api a = new Api("1a3c2fa6735f089a2a1dd4fa11067807383bd08c", "5a0ae446a98a1");
            Console.WriteLine(a.Get("sda", parametrs));

            Console.WriteLine("\npress any key to exit the process...");
            Console.ReadKey();
        }
    }
}
