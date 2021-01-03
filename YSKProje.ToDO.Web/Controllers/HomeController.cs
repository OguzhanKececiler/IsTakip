using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;

namespace YSKProje.ToDo.Web.Controllers
{
    public class HomeController :BaseIdentityController
    {
     //  private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        //    private readonly IMapper _mapper;
        private readonly ICustomLogger _customLogger;
        public HomeController(ICustomLogger customLogger,/*IMapper mapper*//*,IGorevService gorevService*/ UserManager<AppUser> userManager, SignInManager<AppUser> signInManager):base(userManager)//dependency injectionla doluyo
        {
            customLogger = _customLogger;
            //_mapper = mapper;
            //_gorevService = gorevService;//Burdada Startupda yaptıgım nesne olusturma mimarisine gore olusturur ve bak o mimariler sadece contructra ozgu nesne yapması
          //  _userManager = userManager;
            _signInManager = signInManager;
             
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GirisYap(AppUserSignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetirGirisYapanKullanici(model.Username);
                if (user!=null)
                {
                 var IdentityResult=   await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);//sonuncusu  kilitliyimmi belli bir giristen sonra diyorum
                    if (IdentityResult.Succeeded)
                    {//rolunu getirmem gerekiyo
                        var Roller= await   _userManager.GetRolesAsync(user);
                        if (Roller.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        if (Roller.Contains("Member"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Member" });
                        }
                         
                    }
                }
                ModelState.AddModelError("", "Kullanıcı adi veya şifre hatalı");
            }

            return View("Index", model);
        }

        public IActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> KayitOl( AppUserAddDto model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname
                };
                var result=  await    _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    //rol atıcaz sımdı
                  var addRoleResult= await _userManager.AddToRoleAsync(user, "Member");
                    if (addRoleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                
                    HataEkle(addRoleResult.Errors);

                }
            
                HataEkle(result.Errors);
            }


            return View();
        }


        public async Task<IActionResult> CikisYap()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");

        }

        public IActionResult  StatusCode(int? code)
        {
            if (code==404)
            {
                ViewBag.Code = code;
                ViewBag.Message = "Sayfa bulunamadı";
            }
         
            return View();
        }

        public IActionResult Error()
        {
            var exceptionHandler= HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _customLogger.LogError($"Hatanın Oluştuğu yer :{exceptionHandler.Path} \n" +
                $"Hatanın mesajı :{exceptionHandler.Error.Message} \n " +
                $"Stack Trace : {exceptionHandler.Error.StackTrace}");
           ViewBag.Path= exceptionHandler.Path;//hata nerde gerceklesti
            ViewBag.Message = exceptionHandler.Error.Message;
            return View();

        }
         
    }
}
