using Microsoft.AspNetCore.Identity;

namespace Vendas_Site.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _UserM;
        private readonly RoleManager<IdentityRole> _RoleM;

        public SeedUserRoleInitial(UserManager<IdentityUser> userM, RoleManager<IdentityRole> roleM)
        {
            _UserM = userM;
            _RoleM = roleM;
        }

        public void SeedRoles()
        {
            if (!_RoleM.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole { Name = "Member", NormalizedName = "MEMBER" };

              IdentityResult IdenResult = _RoleM.CreateAsync(role).Result;
            }
            if (!_RoleM.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" };

                IdentityResult IdenResult = _RoleM.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            if (_UserM.FindByEmailAsync("usuario@localHost.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _UserM.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded)
                {
                    _UserM.AddToRoleAsync(user, "Member").Wait();
                }
            }

            if (_UserM.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _UserM.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded)
                {
                    _UserM.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
