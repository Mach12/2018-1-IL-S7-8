using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore
{
    public interface IDatabaseCurrentUserInfo
    {
        bool GetConsentToWinOrLoose();

    }

    public class WinOrLooseUserConsentService : IWinOrLooseService
    {
        readonly IDatabaseCurrentUserInfo _userDb;

        public WinOrLooseUserConsentService( IDatabaseCurrentUserInfo userDb )
        {
            _userDb = userDb;
        }

        public bool Win( HttpContext c )
        {
            if( _userDb.GetConsentToWinOrLoose() )
            {
                return Environment.TickCount % 2 == 0;
            }
            return true;
        }

    }
}
