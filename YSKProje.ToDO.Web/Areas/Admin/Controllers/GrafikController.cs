using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class GrafikController : Controller
    {
     private readonly   IAppUserService _appUserService;
        public GrafikController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = TempDataInfo.Grafik;



            return View();
        }

        public IActionResult EnCokTamamlayan()
        {
          var jsonstring=  JsonConvert.SerializeObject(_appUserService.EnCokGorevTamamlamisPersoneller());
            return Json(jsonstring);
        }
        public IActionResult EnCokCalisan()
        {
            var jsonstring = JsonConvert.SerializeObject(_appUserService.EnCokGorevdeCalisanPersoneller());
            return Json(jsonstring);
        }
    }
}
