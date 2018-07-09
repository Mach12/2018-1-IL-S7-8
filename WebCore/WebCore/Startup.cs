using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            services.AddAuthentication()
                .AddCookie( "MonCookie", c =>
                {
                    c.Cookie.Name = "Pouf";
                    c.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

                } )
                .AddGoogle( "Google", options =>
                {
                    options.ClientId = "1012618945754-fi8rm641pdegaler2paqgto94gkpp9du.apps.googleusercontent.com";
                    options.ClientSecret = "vRALhloGWbPs7PJ5LzrTZwkH";
                    options.Events.OnTicketReceived = ticketReceivedContext => 
                    {
                        ticketReceivedContext.Principal
                        return Task.CompletedTask;
                    };
                    options.SignInScheme = "MonCookie";
                } );
            services.Configure<WinOrLooseOptions>( _configuration.GetSection( "WinOrLoose" ) );
            services.AddScoped<IWinOrLooseService, DefaultWinOrLooseService>();
        }

        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            app.UseAuthentication();


            app.Use( async ( context, next ) =>
            {
                if( context.Request.Path.StartsWithSegments( "/loginGoogle" ) )
                {
                    await context.ChallengeAsync( "Google" );
                    return;
                }
                else if( context.Request.Path.StartsWithSegments( "/login" ) )
                {
                    var identity = new ClaimsIdentity();
                    identity.AddClaim( new Claim( "name", "toto" ) );
                    ClaimsPrincipal principal = new ClaimsPrincipal( identity );
                    await context.SignInAsync( "SuperCookie", principal );
                }
                else if( context.Request.Path.StartsWithSegments( "/logout" ) )
                {
                    await context.SignOutAsync( "SuperCookie" );
                }
                await next();
            } );

            //app.Map( "/loto", app2 =>
            //{
            //    app2.UseMiddleware<WinOrLooseMiddleware>();
            //    app2.Run( async context => await context.Response.WriteAsync( "Hello World! (IN LOTO!)" ) );
            //} );

            app.MapWhen( c => c.Request.Path.StartsWithSegments( "/loto" )
                                &&
                              !(c.Request.Query["loto"] != "none" ), app2 =>
            {
                app2.UseMiddleware<WinOrLooseMiddleware>();
                app2.Run( async context => await context.Response.WriteAsync( "Hello World! (IN LOTO!)" ) );
            } );


            app.Run( async context => await context.Response.WriteAsync( "Hello World!" ) );
        }

    }
}
