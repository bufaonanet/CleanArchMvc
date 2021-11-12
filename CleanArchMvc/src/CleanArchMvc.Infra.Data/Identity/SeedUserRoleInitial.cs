using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInicial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            const string USER_LOCAL = "usuario@localhost";

            if (_userManager.FindByEmailAsync(USER_LOCAL).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = USER_LOCAL,
                    Email = USER_LOCAL,
                    NormalizedEmail = USER_LOCAL.ToUpper(),
                    NormalizedUserName = USER_LOCAL.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = _userManager.CreateAsync(user, "User123@").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            const string ADMIN_LOCAL = "admin@localhost";

            if (_userManager.FindByEmailAsync(ADMIN_LOCAL).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = ADMIN_LOCAL,
                    Email = ADMIN_LOCAL,
                    NormalizedEmail = ADMIN_LOCAL.ToUpper(),
                    NormalizedUserName = ADMIN_LOCAL.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = _userManager.CreateAsync(user, "Admin123@").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "USER";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }
    }
}