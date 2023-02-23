using System;
using System.Net;
using System.Threading.Tasks;

namespace lab5_pppi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string API_ACCESS_KEY = Environment.GetEnvironmentVariable("PPPI_UNSPLASH_ACCESS_KEY");
            string API_SECRET_KEY = Environment.GetEnvironmentVariable("PPPI_UNSPLASH_SECRET_KEY");
            UnsplashAPIController unsplash = new UnsplashAPIController(API_ACCESS_KEY, API_SECRET_KEY);

            while (true)
            {
                Console.Write("\nEnter your query OR 'EXIT' to exit: ");
                string query = Console.ReadLine();
                if (query == "EXIT") break;

                var response = await unsplash.GetRandomPhoto(query);
                Console.WriteLine("==============================");
                if (response.statusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine($"=\t[{query}] PHOTO INFORMATION");
                    response.printData();
                    Console.WriteLine($"===========");
                    Console.WriteLine($"= Do you want to like it? YES or NO");
                    bool answer = Console.ReadLine().ToUpper() == "YES";
                    if (answer)
                    {
                        var result = await unsplash.LikePhoto(response.data.id);
                        if (result.statusCode == HttpStatusCode.Created)
                        {
                            Console.WriteLine("= Succesful! Thanks for your like!");
                        } else
                        {
                            Console.WriteLine("= Oh... Something went wrong\n" +
                                "= It seems like your code wasn't valid!");
                        }
                    }
                    Console.WriteLine($"===========\nThanks for viewing this photo!");
                } else
                {
                    Console.WriteLine("= Invalid query!");
                }
                Console.WriteLine("==============================\n");
            }
            Console.ReadKey();
        }
    }
}
