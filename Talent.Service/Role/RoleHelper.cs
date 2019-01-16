using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Talent.Service.Role
{
    public static class RoleHelper
    {
        public static bool IsSkinAdmin()
        {
            return Roles.GetRolesForUser().Contains(SkinRoles.SKIN_ADMIN);
        }

        public static bool HasAccessToUsers()
        {
            return Roles.GetRolesForUser().Any(x => UsersRoles.Contains(x));
        }

        private static IEnumerable<string> UsersRoles { get { return new[] { SkinRoles.SKIN_ADMIN, SkinRoles.SKIN_CUSTOMER, SkinRoles.SKIN_SUPPORT }; } }
    }
}
