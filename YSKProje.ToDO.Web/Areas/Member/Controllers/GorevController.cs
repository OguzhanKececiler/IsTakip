using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(AreaInfo.Member)]
    public class GorevController : BaseIdentityController
    {
       private readonly IGorevService _gorevService;
       // private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public GorevController(IMapper mapper,IGorevService gorevService, UserManager<AppUser> userManager):base(userManager)//Burada diyorumki bak usermanager parametresi base kısmına gitsen orda nesne olsun
        {
            _mapper = mapper;
           // _userManager = userManager;
            _gorevService = gorevService;
        }
        public async Task<IActionResult> Index(int aktifSayfa=1)
        {
            TempData["Active"] = TempDataInfo.Gorev;
            var user = await GetirGirisYapanKullanici();
            List<Gorev> gorevler = _gorevService.GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, user.Id,aktifSayfa);
          

            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.AktifSayfa = aktifSayfa;

           var models= _mapper.Map<List<GorevListAllDto>>(gorevler);

            //List<GorevListAllViewModel> models = new List<GorevListAllViewModel>();
            //foreach (var gorev in gorevler)
            //{
            //    GorevListAllViewModel model = new GorevListAllViewModel();
            //    model.Ad = gorev.Ad;
            //    model.Aciliyet = gorev.Aciliyet;
            //    model.Aciklama = gorev.Aciklama;
            //    model.AppUser = gorev.AppUser;
            //    model.Id = gorev.Id;
            //    model.OlusturulmaTarih = gorev.OlusturulmaTarih;
            //    model.Raporlar = gorev.Raporlar;
            //    models.Add(model);
            //}
            return View(models);
        }
    }
}
