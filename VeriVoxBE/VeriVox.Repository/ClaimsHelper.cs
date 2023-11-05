using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Repository
{
    public class ClaimsHelper
    {
        public static Guid GetUserIdFromClaims(IEnumerable<Claim> claims)
        {
            var userIdClaim = claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
            return Guid.Empty;
        }

        public static int GetRoleFromClaims(IEnumerable<Claim> claims)
        {
            var roleClaim = claims.FirstOrDefault(c => c.Type == "Role");
            if (roleClaim != null && int.TryParse(roleClaim.Value, out int role))
            {
                return role;
            }
            return 0;
        }
    }
}
