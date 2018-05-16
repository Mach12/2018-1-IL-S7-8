using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
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
                .ConfigureAppConfiguration( (hostBuilderContext, confBuilder ) =>
                {
                    confBuilder
                        .AddJsonFile( "appSettings.json", optional: false, reloadOnChange: true )
                        .AddEnvironmentVariables( "InTech_" );

                } )
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
