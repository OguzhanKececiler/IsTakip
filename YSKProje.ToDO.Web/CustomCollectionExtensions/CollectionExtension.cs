using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Concrete;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Business.ValidationRules.FluentValidation;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.DTO.DTOs.AciliyetDtos;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.DTO.DTOs.RaporDtos;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.CustomCollectionExtensions
{
    public static class CollectionExtension
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            //appuser ve approle kullanıcam ve entityframeforkkullanıcam onun ıcındede todocontext db modelımı kullanıcam
            services.AddIdentity<AppUser, AppRole>(opt => {
                opt.Password.RequireDigit = false;//sayı icerme zorunlulugunu kaldırdım
                opt.Password.RequireUppercase = false;//buyuk harf icerme zorunlulugu
                opt.Password.RequiredLength = 1;//en az kabul edilebilir şifre karakter sayısı
                opt.Password.RequireLowercase = false;//kücük harf icerme zorunlulugu
                opt.Password.RequireNonAlphanumeric = false;//soru isareti unlem gibi karakter isteme zorunlulugunuda false yapıyorum
            }).AddEntityFrameworkStores<TodoContext>();

            //kullanacagım cookie yi configure ediyorum
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "IsTakipCookie";
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;//Bu cookie yi baska web sayfalaro paylasamasın dıyoruz
                opt.Cookie.HttpOnly = true;//javascrpt üzerinden ulasılmasını istemiyorum
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);//20 gun boyunca bu cookie kalsın diyorum
                opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;//hangi tür istek gelirse ona gore davran diyorum;
                opt.LoginPath = "/Home/Index";
            });


        }


        public static void AddCustomValidator(this IServiceCollection services)
        {

            services.AddTransient<IValidator<AciliyetAddDto>, AciliyetAddValidator>();//diyorumkı buradakı dto kurallarını aciliyetaddvalidator classından alsın
            services.AddTransient<IValidator<AciliyetUpdateDto>, AciliyetUpdateValidator>();
            services.AddTransient<IValidator<AppUserAddDto>, AppUserAddValidator>();
            services.AddTransient<IValidator<AppUserSignInDto>,AppUserSignInValidator>();
            services.AddTransient<IValidator<GorevAddDto>, GorevAddValidator>();
            services.AddTransient<IValidator<GorevUpdateDto>, GorevUpdateValidator>();
            services.AddTransient<IValidator<RaporAddDto>,RaporAddValidator>();
            services.AddTransient<IValidator<RaporUpdateDto>,RaporUpdateValidator>();

        }

    }
}
