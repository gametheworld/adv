using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class ClaimsADVSession
    {
        public string UserInfo
        {
            get
            {
                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
                if (claimsPrincipal == null)
                {
                    return null;
                }
                var client = claimsPrincipal.FindFirst("ADV.UserInfo");
                if (client == null)
                {
                    return null;
                }
                return claimsPrincipal.FindFirst("ADV.UserInfo").Value;
            }
        }
    }
}
