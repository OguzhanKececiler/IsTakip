using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    //Admin olanlar içeri girebilir diyorum
    [Authorize(Roles =RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class HomeController : BaseIdentityController
    {

        private readonly IBildirimService _bildirimService;
        private readonly IGorevService _gorevService;
        //private readonly UserManager<AppUser> _userManager;
        private readonly IRaporService _raporService;
        public HomeController(IRaporService raporService,IGorevService gorevService,UserManager<AppUser> userManager, IBildirimService bildirimService):base(userManager)//gondermen gerekenparametreyı buraya gonderıyo base dedıgınde
        {
            _raporService = raporService;
           //_userManager= userManager;
            _bildirimService = bildirimService;
            _gorevService = gorevService;
        }
        public async Task< IActionResult> Index()
        {
            TempData["Active"] =TempDataInfo.Anasayfa;
            var user = await GetirGirisYapanKullanici();
            ViewBag.AtanmayiBekleyenGorevSayisi=  _gorevService.GetirAtanmayiBekleyenGorevSayisi();
            ViewBag.TamamlanmisGorevSayisi = _gorevService.GetirTamamlanmisGorevSayisi();
            ViewBag.OkunmamisBildirimSayisi = _bildirimService.GetirOkunmayanBildirimSayisiileAppUserId(user.Id);
            ViewBag.ToplamRaporSayisi = _raporService.GetirRaporSayisi();
            return View();
        }
    }
}
