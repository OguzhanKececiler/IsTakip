using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IAciliyetService _aciliyetService;
        private readonly IMapper _mapper;
        public GorevController(IMapper mapper,IGorevService gorevService, IAciliyetService aciliyetService)
        {
            _mapper = mapper;
            _aciliyetService = aciliyetService;
            _gorevService = gorevService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = TempDataInfo.Gorev; //Get metodlarına koyuyoruz
            //List<Gorev> gorevler = _gorevService.GetirAciliyetTamamlayan();
            //List<GorevListViewModel> models = new List<GorevListViewModel>();
            //foreach (Gorev item in gorevler)
            //{
            //    GorevListViewModel model = new GorevListViewModel
            //    {
            //        Aciklama = item.Aciklama,
            //        Aciliyet = item.Aciliyet,
            //        AciliyetId = item.AciliyetId,
            //        Ad = item.Ad,
            //        Durum = item.Durum,
            //        Id = item.Id,
            //        OlusturulmaTarih = item.OlusturulmaTarih
            //    };
            //    models.Add(model);
            //}
           var data= _mapper.Map<List<GorevListDto>>(_gorevService.GetirAciliyetTamamlayan());
            return View(data);
        }
        public IActionResult EkleGorev()
        {
            TempData["Active"] = TempDataInfo.Gorev; //Get metodlarına koyuyoruz


            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim");//Ne secilsin ne gonderilsin
            return View(new GorevAddDto());
        }
        [HttpPost]
        public IActionResult EkleGorev(GorevAddDto Model)
        {
            if (ModelState.IsValid)
            {
                _gorevService.Kaydet(new Gorev
                {
                    Aciklama = Model.Aciklama,
                    Ad = Model.Ad,
                    AciliyetId = Model.AciliyetId

                });
                return RedirectToAction("Index");
            }
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim");//Ne secilsin ne gonderilsin
            return View(Model);
        }

        public IActionResult GuncelleGorev(int id)
        {
            TempData["Active"] = TempDataInfo.Gorev; //Get metodlarına koyuyoruz

            Gorev gorev = _gorevService.GetirIdile(id);

            var data=_mapper.Map<GorevUpdateDto>(_gorevService.GetirIdile(id));//gelen datayı bana gorevUpdateDto ya cevir diyorum
            //GorevUpdateViewModel Model = new GorevUpdateViewModel
            //{
            //    Id = gorev.Id,
            //    Aciklama = gorev.Aciklama,
            //    AciliyetId = gorev.AciliyetId,
            //    Ad = gorev.Ad
            //};
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim", gorev.AciliyetId);
            return View(data);
        }

        [HttpPost]
        public IActionResult GuncelleGorev(GorevUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                _gorevService.Guncelle(new Gorev
                {
                    Aciklama = model.Aciklama,
                    AciliyetId = model.AciliyetId,
                    Ad = model.Ad,
                    Id = model.Id
                });
                return RedirectToAction("Index");
             
            }
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim", model.AciliyetId);
            return View(model);

        }

        public IActionResult SilGorev(int id)
        {
            _gorevService.Sil(new Gorev { Id = id });
            return Json(null);
        }
    }
}
