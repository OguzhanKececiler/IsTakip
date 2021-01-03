using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Concrete;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.BildirimDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(AreaInfo.Member)]
    public class BildirimController :BaseIdentityController
    {
        private readonly IBildirimService _bildirimService;
        //private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public BildirimController(IMapper mapper,UserManager<AppUser> userManager, IBildirimService bildirimService):base(userManager)
        {
            _mapper = mapper;
            _bildirimService = bildirimService;
           // _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempDataInfo.Bildirim;
            var user = await GetirGirisYapanKullanici();
            List<Bildirim> bildirimler = _bildirimService.GetirOkunmayanlar(user.Id);
            var models= _mapper.Map<List<BildirimListDto>>(bildirimler);
            //List<BildirimListViewModel> models = new List<BildirimListViewModel>();
            //foreach (var item in bildirimler)
            //{
            //    BildirimListViewModel model = new BildirimListViewModel();
            //    model.Id = item.Id;
            //    model.Aciklama = item.Aciklama;
            //    models.Add(model);
            //}

            return View(models);
        }
        [HttpPost]
        public IActionResult Index(int Id)
        {
            var guncellenecekBildirim = _bildirimService.GetirIdile(Id);
            guncellenecekBildirim.Durum = true;
            _bildirimService.Guncelle(guncellenecekBildirim);
            return RedirectToAction("Index");
        }
    }
}
