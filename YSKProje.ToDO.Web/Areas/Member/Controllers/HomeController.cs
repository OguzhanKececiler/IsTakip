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

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(AreaInfo.Member)]
    public class HomeController :BaseIdentityController
    {
        private readonly IRaporService _raporService;
        private readonly IGorevService _gorevService;
     //   private readonly UserManager<AppUser> _userManager;
        private readonly IBildirimService _bildirimService;
        public HomeController(IBildirimService  bildirimService,IGorevService  gorevService, UserManager<AppUser> userManager, IRaporService raporService):base(userManager)
        {
            _bildirimService = bildirimService;
           _gorevService= gorevService;
           // _userManager = userManager;
            _raporService = raporService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] =TempDataInfo.Anasayfa;
            var user = await GetirGirisYapanKullanici();
            ViewBag.RaporSayisi = _raporService.GetirRaporSayisiileAppUserId(user.Id);
            ViewBag.TamamlananGorevSayisi = _gorevService.GetirGorevSayisiileTamamlananileAppUserId(user.Id);
            ViewBag.OkunmamisBildirimSayisi = _bildirimService.GetirOkunmayanBildirimSayisiileAppUserId(user.Id);
            ViewBag.TamamlanmasiGerekenGorevSayisi = _gorevService.GetirGorevSayisiTamamlanmasiGerekenileAppUserId(user.Id);

            return View();
        }
    }
}
