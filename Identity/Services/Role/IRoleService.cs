using Identity.Models.Dtos.Role;
using Identity.Models.Entities;

namespace Identity.Services.Role
{
    public interface IRoleService
    {
        Task<bool> CreateRole(CreateRoleDto role);
        Task<List<AppRole>> GetRoles();
        Task<bool> RoleAssign(AppUser user, string Role);
    }
}
