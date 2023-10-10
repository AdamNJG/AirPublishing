using ImageStorage.ImageVerification.Repository;
using ImageStorage.ImageVerification.Service;
using ImageStorage.ImageVerification.Storage;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Reflection;

namespace ImageStorage.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreatHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreatHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}