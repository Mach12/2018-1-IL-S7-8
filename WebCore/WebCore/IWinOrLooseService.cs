using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCore
{
    public interface IWinOrLooseService
    {
        Task<bool> Win( HttpContext c );
    }


    public class DefaultWinOrLooseService : IWinOrLooseService
    {
        readonly IOptionsSnapshot<WinOrLooseOptions> _options;

        public DefaultWinOrLooseService( IOptionsSnapshot<WinOrLooseOptions> options )
        {
            _options = options;
        }

        public Task<bool> Win( HttpContext c )
        {
            return Task.FromResult( Environment.TickCount % _options.Value.OneOutOf == 0 );
        }
    }

}
