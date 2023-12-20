using Identity.Models.Dtos.Role;
using Identity.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services.Role
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;
        readonly UserManager<AppUser> _userManager;

        public RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> CreateRole(CreateRoleDto role)
        {
            if (role == null)
            {
                throw new Exception();
            }

            IdentityResult result = await _roleManager.CreateAsync(new AppRole() { Name = role.Role });
            return result.Succeeded;
        }
        public async Task<List<AppRole>> GetRoles() => await _roleManager.Roles.ToListAsync();

        public async Task<bool> RoleAssign(AppUser user, string Role)
        {
            if (user == null || Role == null) throw new Exception();
            user = await _userManager.FindByEmailAsync(user.Email);
            if (await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == Role) == null)
            {

                await _roleManager.CreateAsync(new AppRole() { Name = Role });
            }

            IdentityResult result = await _userManager.AddToRoleAsync(user, Role);

            return result.Succeeded;
        }
    }
}

