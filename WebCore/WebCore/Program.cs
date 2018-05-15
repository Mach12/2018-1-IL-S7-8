using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace WebCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>();

            IWebHost host = builder.Build();
            host.Run();
        }
    }
}
