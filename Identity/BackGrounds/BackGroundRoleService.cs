using Identity.Configurations;
using Identity.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Identity.BackGrounds
{
    public  class BackGroundRoleService
    {
        public async static Task CreateRoles(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            foreach (AppRole role in Constants.AllRoles)
            {
                bool result = await _roleManager.RoleExistsAsync(role.Name);
                if (!result)
                {

                    await _roleManager.CreateAsync(role);

                }
            }
        }
    }
}
