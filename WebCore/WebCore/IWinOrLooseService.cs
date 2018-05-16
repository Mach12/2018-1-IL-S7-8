using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore
{
    public interface IWinOrLooseService
    {
        bool Win( HttpContext c );
    }


    public class DefaultWinOrLooseService : IWinOrLooseService
    {
        int _rate;

        public DefaultWinOrLooseService( IOptions<WinOrLooseOptions> options )
        {
            _rate = options.Value.OneOutOf;
        }

        public bool Win( HttpContext c )
        {
            return Environment.TickCount % _rate == 0;
        }
    }

}
