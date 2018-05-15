using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore
{
    class Startup
    {
        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            app.UseMiddleware<WinOrLooseMiddleware>();

            app.Run( async context => await context.Response.WriteAsync( "Hello World!" ) );
        }

    }
}
