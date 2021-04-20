using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Phonebook
{
    // Master program class, we start here
    public class Program
    {
        // system entry point
        public static void Main(string[] args)
        {
            // standart dotnet builder
            CreateHostBuilder(args).Build().Run();
        }

        // here we can specify some basic config if we wants
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // we start from typical behavior   
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // use class named Startup -- it is typical too
                    webBuilder.UseStartup<Startup>();
                });
    }
}
