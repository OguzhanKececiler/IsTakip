using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.ViewComponents
{
    public class Wrapper:ViewComponent
    {//hepsini tek bi yerde yazıp viewlerini dallandırıp budaklandırabılıyorsun ana viewde olsa sorun olmaz böle yazman daha mantııklı
        private readonly UserManager<AppUser> _userManager;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;
        public Wrapper(  IMapper mapper,UserManager<AppUser> userManager,IBildirimService bildirimService)
        {
            _mapper = mapper;
           _bildirimService= bildirimService;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {//View tarafında shareda ViewComponents diye bi dosya altında class ismiyle bir dosya sonra bir tane partialview ve ismi herzaman default yazılacak
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;//sekronikmis gibi
         var model=   _mapper.Map<AppUserListDto>(user);
            //AppUserListViewModel model = new AppUserListViewModel();
            //model.Id = user.Id;
            //model.Name = user.Name;
            //model.Picture = user.Picture;
            //model.SurName = user.Surname;
            //model.Email = user.Email;

            var bildirimler = _bildirimService.GetirOkunmayanlar(user.Id).Count();
            ViewBag.Bildirimsayisi = bildirimler;

            var Roles = _userManager.GetRolesAsync(user).Result;

            if (Roles.Contains("Admin"))
            {
                return View(model);//Rolesum eger admini iceriyosa giriste ordan cagırılmıssan dırek defaulta git
            }
            return View("Member", model);//wrapper classımın viewi olan member tarafına git diyorm
        }
    }
}
