using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore
{
    public class WinOrLooseIPBasedService : IWinOrLooseService
    {
        public bool Win( HttpContext c )
        {
            if( c.Connection.RemoteIpAddress.IsIPv6SiteLocal )
            {
                return Environment.TickCount % 2 == 0;
            }
            return true;
        }

    }
}
