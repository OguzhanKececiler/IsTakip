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
using YSKProje.ToDo.DTO.DTOs.RaporDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(AreaInfo.Member)]
    public class IsEmriController : BaseIdentityController
    {
        private readonly IBildirimService _bildirimService;
       // private readonly UserManager<AppUser> _userManager;
        private readonly IGorevService _gorevService;
        private readonly IRaporService _raporService;
        private readonly IMapper _mapper;
        public IsEmriController(IMapper mapper,IRaporService raporService, IGorevService gorevService, UserManager<AppUser> userManager, IBildirimService bildirimService):base(userManager)
        {
            _mapper = mapper;
            _bildirimService = bildirimService;
            _raporService = raporService;
            //_userManager = userManager;
            _gorevService = gorevService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempDataInfo.IsEmri;
            var user = await GetirGirisYapanKullanici();//iceri giren userı bul
            var Gorev = _gorevService.GetirTumTablolarla(x => x.AppUserId == user.Id && !x.Durum);

            var Models = _mapper.Map<List<GorevListAllDto>>(Gorev);
            
            //List<GorevListAllViewModel> Models = new List<GorevListAllViewModel>();

            //foreach (Gorev item in Gorev)
            //{
            //    GorevListAllViewModel Model = new GorevListAllViewModel();
            //    Model.Id = item.Id;
            //    Model.Aciklama = item.Aciklama;
            //    Model.Aciliyet = item.Aciliyet;
            //    Model.Ad = item.Ad;
            //    Model.AppUser = item.AppUser;
            //    Model.Raporlar = item.Raporlar;
            //    Model.OlusturulmaTarih = item.OlusturulmaTarih;
            //    Models.Add(Model);
            //}
            return View(Models);
        }
        public IActionResult EkleRapor(int id)
        {
            TempData["Active"] = TempDataInfo.IsEmri;


            var gorev = _gorevService.GetirAciliyetileId(id);
            RaporAddDto model = new RaporAddDto
            {
                GorevId = id,
                Gorev = gorev
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EkleRapor(RaporAddDto model)
        {
            if (ModelState.IsValid)
            {
                _raporService.Kaydet(new Rapor()
                {
                    GorevId = model.GorevId,
                    Detay = model.Detay,
                    Tanim = model.Tanim
                });

                IList<AppUser> AdminOlanKullanicilar = await _userManager.GetUsersInRoleAsync("Admin");
                AppUser AktifKullanici = await GetirGirisYapanKullanici();
                foreach (var admin in AdminOlanKullanicilar)
                {
                    _bildirimService.Kaydet(new Bildirim
                    {
                        Aciklama = $"{AktifKullanici.Name} {AktifKullanici.Surname} yeni bir rapor yazdı.",
                        AppUserId = admin.Id
                    });
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult GuncelleRapor(int id)
        {
            TempData["Active"] = TempDataInfo.IsEmri;


            var gorev = _raporService.GetirGorevileID(id);
            RaporUpdateDto model = new RaporUpdateDto
            {
                Id = gorev.Id,
                Tanim = gorev.Tanim,
                Detay = gorev.Detay,
                Gorev = gorev.Gorev,
                GorevId = gorev.GorevId
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult GuncelleRapor(RaporUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekRapor = _raporService.GetirIdile(model.Id);
                guncellenecekRapor.Tanim = model.Tanim;
                guncellenecekRapor.Detay = model.Detay;
                _raporService.Guncelle(guncellenecekRapor);
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public async Task<IActionResult> TamamlaGorev(int gorevId)
        {
            var guncellenecekGorev = _gorevService.GetirIdile(gorevId);
            guncellenecekGorev.Durum = true;
            _gorevService.Guncelle(guncellenecekGorev);
            var AdminOlanKullanicilar = await _userManager.GetUsersInRoleAsync("Admin");
            var AktifKullanici = await GetirGirisYapanKullanici();
            foreach (var admin in AdminOlanKullanicilar)
            {
                _bildirimService.Kaydet(new Bildirim
                {
                    Aciklama = $"{AktifKullanici.Name} {AktifKullanici.Surname}  görevi tamamladı",
                    AppUserId = admin.Id
                });
            }
            return Json(null);
        }
    }
}
