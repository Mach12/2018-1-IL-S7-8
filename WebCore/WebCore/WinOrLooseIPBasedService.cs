using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCore
{
    public class WinOrLooseIPBasedService : IWinOrLooseService
    {
        public Task<bool> Win( HttpContext c )
        {
            if( c.Connection.RemoteIpAddress.IsIPv6SiteLocal )
            {
                return Task.FromResult( Environment.TickCount % 2 == 0 );
            }
            return Task.FromResult( true );
        }

    }
}
