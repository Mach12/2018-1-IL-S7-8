using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCore
{
    public interface IDatabaseCurrentUserInfo
    {
        Task<bool> GetConsentToWinOrLoose();

    }

    public class WinOrLooseUserConsentService : IWinOrLooseService
    {
        readonly IDatabaseCurrentUserInfo _userDb;

        public WinOrLooseUserConsentService( IDatabaseCurrentUserInfo userDb = null )
        {
            _userDb = userDb;
        }

        public async Task<bool> Win( HttpContext c )
        {
            if( await _userDb.GetConsentToWinOrLoose() )
            {
                return Environment.TickCount % 2 == 0;
            }
            return true;
        }

    }
}
