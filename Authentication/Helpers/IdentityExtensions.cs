using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Authentication.Helpers
{
    /// <summary>
    /// IdentityExtensions
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// GetUserId
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static long GetUserId(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.Name);

            return claim == null ? 0 : Convert.ToInt64(claim.Value);
        }

        /// <summary>
        /// GetRole
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetRole(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.Role);

            return claim?.Value ?? string.Empty;
        }
    }
}