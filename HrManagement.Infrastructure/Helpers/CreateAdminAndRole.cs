using HrManagement.Application.Constant;
using HrManagement.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace HrManagement.Infrastructure.Helpers;

public struct CreateAdminAndRole()
{
    public static async Task CreateAdminAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new AppRole { Name = RoleConstant.Admin });
            await roleManager.CreateAsync(new AppRole { Name = EmployeeConstant.Employee });
            await roleManager.CreateAsync(new AppRole { Name = EmployeeConstant.HumanResources });
            await roleManager.CreateAsync(new AppRole { Name = EmployeeConstant.Manager });
        }

        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                NameSurname = "Admin",
                UserName = "Admin",
                Email = "admin@admin.com",
                Id = Guid.NewGuid()
            };
            await userManager.CreateAsync(user, "Admin1,");
            await userManager.AddToRoleAsync(user, RoleConstant.Admin);
        }
    }
}