using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.BaseControllers
{
    public class BaseIdentityController : Controller
    {
        protected readonly UserManager<AppUser> _userManager;
        public BaseIdentityController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
      
       protected async Task< AppUser> GetirGirisYapanKullanici()
        {
          return await    _userManager.FindByNameAsync(User.Identity.Name);
        }

        protected async Task<AppUser> GetirGirisYapanKullanici( string name)
        {
            return await _userManager.FindByNameAsync(name);
        }
        protected void HataEkle(IEnumerable<IdentityError> errors)
        {
            foreach (var item in errors)
            {
                ModelState.AddModelError("", item.Description);

            }

        }
    }
}
