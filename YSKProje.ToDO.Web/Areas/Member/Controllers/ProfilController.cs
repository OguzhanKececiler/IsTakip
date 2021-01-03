using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(AreaInfo.Member)]
    public class ProfilController : BaseIdentityController
    {
       // private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public ProfilController(IMapper mapper,UserManager<AppUser> userManager):base(userManager)
        {
        _mapper=    mapper;
          //  _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempDataInfo.Profil;
            /*Burda buraya gırmıs olan user oluyo**/
            AppUser appUser = await GetirGirisYapanKullanici();

            var model = _mapper.Map<AppUserListDto>(appUser);
            //AppUserListViewModel model = new AppUserListViewModel();
            //model.Id = appUser.Id;
            //model.Name = appUser.Name;
            //model.Email = appUser.Email;
            //model.SurName = appUser.Surname;
            //model.Picture = appUser.Picture;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserListDto Model,IFormFile Resim)
        {
            if (ModelState.IsValid)
            {
              var GuncellenecekKullanici=  _userManager.Users.FirstOrDefault(x=>x.Id== Model.Id);
                if (Resim!=null)
                {
                    string uzanti = Path.GetExtension(Resim.FileName);
                    string resimAd = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + resimAd);
                    using (var stream=new FileStream(path,FileMode.Create))
                    {
                        await Resim.CopyToAsync(stream);
                    }

                    GuncellenecekKullanici.Picture = resimAd;
                }
                GuncellenecekKullanici.Name = Model.Name;
                GuncellenecekKullanici.Surname = Model.SurName;
                GuncellenecekKullanici.Email = Model.Email;

             var result=  await _userManager.UpdateAsync(GuncellenecekKullanici);
                if (result.Succeeded)
                {
                    TempData["message"] = "Güncelleme işlemi başarılı";
                    return RedirectToAction("Index");
                }
                HataEkle(result.Errors);
            }
            return View(Model);
        }
        }
}
