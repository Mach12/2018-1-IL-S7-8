using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore
{
    class Startup
    {
        public void ConfigureServices( IServiceCollection services, IConfiguration configuration )
        {
            services.AddOptions();
            services.Configure<WinOrLooseOptions>( configuration.GetSection( "WinOrLoose" ) );
            services.AddScoped<IWinOrLooseService, WinOrLooseUserConsentService>();
        }

        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            app.UseMiddleware<WinOrLooseMiddleware>();

            app.Run( async context => await context.Response.WriteAsync( "Hello World!" ) );
        }

    }
}
