using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace WebCore
{
    class Program
    {
        static void Main( string[] args )
        {
            var builder = new WebHostBuilder()
                .UseContentRoot( Directory.GetCurrentDirectory() )
                .ConfigureLogging( b =>
                {
                    b.AddConsole();
                    b.SetMinimumLevel( LogLevel.Trace );
                } )
                .UseKestrel()
                .UseStartup<Startup>();

            builder.Build().Run();
        }
    }
}
