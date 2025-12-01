using L1_Zvejyba.Data.Auth.Model;
using Microsoft.AspNetCore.Identity;
namespace L1_Zvejyba.Data.Auth
{
    public class AuthSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAsync()
        {
            await AddDefaultRolesAsync();
            await AddAdminUserAsync();

        }

        private async Task AddAdminUserAsync()
        {
            var newAdminUser = new User()
            { 
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
           
            if (existAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1*");
                //Console.WriteLine(createAdminUserResult);
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, UserRoles.All);
                }
                else
                {
                    throw new Exception("Failed to create new admin");
                }
            }


        }

        private async Task AddDefaultRolesAsync()
        {
            foreach(var role in UserRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if(!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
