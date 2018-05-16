using Microsoft.AspNetCore.Http;
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
        public bool Win( HttpContext c )
        {
            return Environment.TickCount % 2 == 0;
        }
    }

}
