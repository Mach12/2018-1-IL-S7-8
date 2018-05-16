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
        readonly IOptionsSnapshot<WinOrLooseOptions> _options;

        public DefaultWinOrLooseService( IOptionsSnapshot<WinOrLooseOptions> options )
        {
            _options = options;
        }

        public bool Win( HttpContext c )
        {
            return Environment.TickCount % _options.Value.OneOutOf == 0;
        }
    }

}
