using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AciliyetDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class AciliyetController : Controller
    {
        private readonly IAciliyetService _aciliyetService;
        private readonly IMapper _mapper;
        public AciliyetController(IAciliyetService aciliyetService,IMapper mapper)
        {
            _mapper = mapper;
            _aciliyetService = aciliyetService;
        }

        public IActionResult Index()
        {
            TempData["Active"] = TempDataInfo.Aciliyet;
            //List<Aciliyet> aciliyetler = _aciliyetService.GetirHepsi();

            //List<AciliyetListViewModel> Model = new List<AciliyetListViewModel>();
            //foreach (var item in aciliyetler)
            //{
            //    AciliyetListViewModel aciliyetModel = new AciliyetListViewModel();
            //    aciliyetModel.Id = item.Id;
            //    aciliyetModel.Tanim = item.Tanim;
            //    Model.Add(aciliyetModel);
            //}

            var data =  _mapper.Map<List< AciliyetListDto>>(_aciliyetService.GetirHepsi());
            return View(data);
        }


        public IActionResult EkleAciliyet()
        {
            TempData["Active"] = TempDataInfo.Aciliyet;

            return View(new AciliyetAddDto());
        }

        [HttpPost]
        public IActionResult EkleAciliyet(AciliyetAddDto model)
        {
            if (ModelState.IsValid)
            {
                _aciliyetService.Kaydet(new Aciliyet()
                {
                    Tanim = model.Tanim
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult GuncelleAciliyet(int id)
        {
            TempData["Active"] = TempDataInfo.Aciliyet;//Get metodlarına koyuyoruz
             //Aciliyet aciliyet = _aciliyetService.GetirIdile(id);

            //AciliyetUpdateViewModel model = new AciliyetUpdateViewModel
            //{
            //    Id = aciliyet.Id,
            //    Tanim = aciliyet.Tanim
            //};
            var data=   _mapper.Map<AciliyetUpdateDto>(_aciliyetService.GetirIdile(id));

            return View(data);
        }

        [HttpPost]
        public IActionResult GuncelleAciliyet(AciliyetUpdateDto Model)
        {
            if (ModelState.IsValid)
            {
                _aciliyetService.Guncelle(new Aciliyet
                {
                    Id = Model.Id,
                    Tanim = Model.Tanim
                });
                return RedirectToAction("Index");
            }
            return View(Model);
        }
    }
}
