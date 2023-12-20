using Identity.Models.Entities;
using System.Security.Claims;

namespace Identity.Configurations
{
    public class Constants
    {
        public static IList<AppRole> AllRoles = new List<AppRole>()
        {
            new AppRole(){ Name = "User" },
            new AppRole(){ Name = "Admin" }
        };

    }
}
