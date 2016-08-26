///Created by: Bartul Kovačić
///Github: https:github.com/BKova
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Evidencija
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseUrls(args)
                .UseStartup<Startup>()
                .Build();
                host.Run();
        }
    }
}
