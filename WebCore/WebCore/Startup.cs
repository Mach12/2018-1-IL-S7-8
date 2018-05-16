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
        readonly IConfiguration _configuration;

        public Startup( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddOptions();
            services.Configure<WinOrLooseOptions>( _configuration.GetSection( "WinOrLoose" ) );
            services.AddScoped<IWinOrLooseService, DefaultWinOrLooseService>();
        }

        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            app.UseMiddleware<WinOrLooseMiddleware>();

            app.Run( async context => await context.Response.WriteAsync( "Hello World!" ) );
        }

    }
}
