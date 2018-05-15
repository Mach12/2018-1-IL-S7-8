using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCore
{
    public class WinOrLooseMiddleware
    {
        readonly ILogger _logger;
        readonly RequestDelegate _next;

        public WinOrLooseMiddleware( RequestDelegate next, ILoggerFactory loggerFactory )
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<WinOrLooseMiddleware>();
        }

        public async Task Invoke( HttpContext context )
        {
            if( Environment.TickCount % 2 == 0 )
            {
                _logger.LogTrace( "!!!! Someone lost !!!!" );
                await context.Response.WriteAsync( "LOOSE!" );
            }
            else
            {
                _logger.LogTrace( "Someone won!" );
                await context.Response.WriteAsync( "OK, you win! => " );
    
                await _next( context );
            }
        }
    }
}
