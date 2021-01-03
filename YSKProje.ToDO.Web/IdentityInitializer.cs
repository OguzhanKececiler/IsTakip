using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {
            //ilk basta benım admin ve role isminde rollerim mevcutmu degilse ekle diyorum
            var AdminRole = await roleManager.FindByNameAsync("Admin");
            if (AdminRole==null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Admin" });
            }
            var memberRole = await roleManager.FindByNameAsync("Member");
            if (memberRole==null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Member" });

            }


            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser==null)
            {
                AppUser user = new AppUser
                {
                    Name = "Oguzhan",
                    Surname = "Kececiler",
                    UserName = "b120910045@sakarya.edu.tr",
                    Email = "oguzhankececiler@gmail.com"
                };
                await userManager.CreateAsync(user, "123");//sen useri olustur parolasınıda 1 yap dıyorum

                await userManager.AddToRoleAsync(user, "Admin");//useri al  ve admin rolune otomatik koy 
            }
        }
    }
}
