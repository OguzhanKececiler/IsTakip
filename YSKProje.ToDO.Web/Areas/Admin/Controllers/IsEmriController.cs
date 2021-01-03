using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.DTO.DTOs.RaporDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class IsEmriController : BaseIdentityController
    {
        private readonly IBildirimService _bildirimService;
        private readonly IAppUserService      _appUserService;
        private readonly IGorevService        _gorevService;
       // private readonly UserManager<AppUser> _userManager;
        private readonly IDosyaService _dosyaService;
        private readonly IMapper _mapper;
        public IsEmriController(IMapper mapper,IBildirimService bildirimService, IAppUserService appUserService, IGorevService gorevService, UserManager<AppUser> userManager, IDosyaService dosyaService):base(userManager)
        {
            _mapper = mapper;
            _bildirimService = bildirimService;
            //_userManager    = userManager;
            _appUserService = appUserService;
            _gorevService   = gorevService;
            _dosyaService = dosyaService;
        }
        public IActionResult GetirExcel(int id)
        {
            return File(_dosyaService.AktarExcel(_mapper.Map<List<RaporDosyaDto>>( _gorevService.GetirRaporlarveAppuserileID(id).Raporlar)), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",Guid.NewGuid()+".xlsx");
        }
        public IActionResult GetirPdf(int id)
        {
          var path=  _dosyaService.AktarPdf(_mapper.Map<List<RaporDosyaDto>>(_gorevService.GetirRaporlarveAppuserileID(id).Raporlar));
            return File(path, "application/pdf", Guid.NewGuid()+".pdf");
        
        
        }


        public IActionResult Index()
        {
            TempData["Active"] =TempDataInfo.IsEmri;
            List<Gorev> gorevler = _gorevService.GetirTumTablolarla();

          var data=  _mapper.Map < List<GorevListAllDto>>(gorevler);
            //List<GorevListAllViewModel> modellist = new List<GorevListAllViewModel>();
            //foreach (var item in gorevler)
            //{
            //    GorevListAllViewModel model = new GorevListAllViewModel
            //    {
            //        Aciklama = item.Aciklama,
            //        Aciliyet = item.Aciliyet
            //    };
            //    model.Aciklama = item.Aciklama;
            //    model.Ad = item.Ad;
            //    model.Id = item.Id;
            //    model.OlusturulmaTarih = item.OlusturulmaTarih;
            //    model.Raporlar = item.Raporlar;
            //    model.AppUser = item.AppUser;
            //    modellist.Add(model);
            //}
            return View(data);
        }

        public IActionResult AtaPersonel(int id,string s,int sayfa=1)
        {
            TempData["Active"] = TempDataInfo.IsEmri;

            ViewBag.AktifSayfa = sayfa;
            ViewBag.ToplamSayfa = (int)Math.Ceiling((Double)_appUserService.GetirAdminOlmayanlar().Count / 3);//yuvarlama yapıyorum 7 tane olunca uye kaybetmemek ıcın
            ViewBag.Aranan = s;

            // List<AppUser> personeller = _appUserService.GetirAdminOlmayanlar(out toplamSayfa, s, sayfa);


            var personeller = _mapper.Map<List<AppUserListDto>>(_appUserService.GetirAdminOlmayanlar(out int toplamSayfa, s, sayfa));
            ViewBag.ToplamSayfa = toplamSayfa;

            //List<AppUserListViewModel> appUserListViewModels = new List<AppUserListViewModel>();
            //foreach (var item in personeller)
            //{
            //    AppUserListViewModel model = new AppUserListViewModel
            //    {
            //        Id = item.Id,
            //        Email = item.Email,
            //        Name = item.Name,
            //        Picture = item.Picture,
            //        SurName = item.Surname
            //    };
            //    appUserListViewModels.Add(model);
            //}
            ViewBag.Personeller = personeller;

            Gorev Gorev = _gorevService.GetirAciliyetileId(id);
          var data=  _mapper.Map<GorevListDto>(Gorev);
            //GorevListViewModel modelGidecek = new GorevListViewModel
            //{
            //    Aciklama = Gorev.Aciklama,
            //    Aciliyet = Gorev.Aciliyet,
            //    Ad = Gorev.Ad,
            //    OlusturulmaTarih = Gorev.OlusturulmaTarih,
            //    Id = Gorev.Id
            //};
            return View(data);
        }

        [HttpPost]
        public IActionResult AtaPersonel(PersonelGorevlendirDto model)
        {
            Gorev guncellenecekGorev =  _gorevService.GetirIdile(model.GorevId);
            guncellenecekGorev.AppUserId = model.PersonelId;
            _bildirimService.Kaydet(new Bildirim
            {
                AppUserId=model.PersonelId,
                Aciklama=$"{guncellenecekGorev.Ad} adlı iş için görevlendirildiniz."
            });
            
            _gorevService.Guncelle(guncellenecekGorev);




            return RedirectToAction("Index");
        }

        public IActionResult GorevlendirPersonel(PersonelGorevlendirDto model)
        {
            TempData["Active"] = TempDataInfo.IsEmri;

            AppUser user = _userManager.Users.FirstOrDefault(x => x.Id == model.PersonelId);
         var userModel=   _mapper.Map<AppUserListDto>(user);
            //AppUserListViewModel userModel = new AppUserListViewModel
            //{
            //    Email = user.Email,
            //    Id = user.Id,
            //    Name = user.Name,
            //    Picture = user.Picture,
            //    SurName = user.Surname
            //};

            Gorev gorev = _gorevService.GetirAciliyetileId(model.GorevId);
            var gorevModel = _mapper.Map<GorevListDto>(gorev);
            //GorevListViewModel gorevModel = new GorevListViewModel
            //{
            //    Id = gorev.Id,
            //    Aciklama = gorev.Aciklama,
            //    Aciliyet = gorev.Aciliyet,
            //    AciliyetId = gorev.AciliyetId,
            //    Ad = gorev.Ad,
            //    //Durum = gorev.Durum,
            //    //OlusturulmaTarih = gorev.OlusturulmaTarih
            //};

            PersonelGorevlendirListDto personelGorevlendirModel = new PersonelGorevlendirListDto
            {
                AppUser = userModel,
                Gorev = gorevModel
            };
            return View(personelGorevlendirModel);
        }


        public IActionResult Detaylandir(int id)
        {
            TempData["Active"] = TempDataInfo.IsEmri;

            Gorev Gorev = _gorevService.GetirRaporlarveAppuserileID(id);

          var data=  _mapper.Map<GorevListAllDto>(Gorev);
            //GorevListAllViewModel model = new GorevListAllViewModel
            //{
            //    Id = Gorev.Id,
            //    AppUser = Gorev.AppUser,
            //    Aciklama = Gorev.Aciklama,
            //    Raporlar = Gorev.Raporlar,
            //    Ad = Gorev.Ad
            //};
            return View(data);
        }
    }
}
