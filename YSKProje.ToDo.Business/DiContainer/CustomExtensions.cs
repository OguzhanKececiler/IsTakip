using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Business.Concrete;
using YSKProje.ToDo.Business.CustomLogger;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.DataAccess.Interfaces;

namespace YSKProje.ToDo.Business.DiContainer
{
    public static  class CustomExtensions
    {
        public static void AddContainerWithDepencies(this IServiceCollection services) //burada artık IserviceCollectiona bir add fonk ekleyebilmis oluorum
        {
            //Igorev servisi gordugunde gorevmanegerı asagıda belırtılen mimarilerden birine gore cagır diyorum
            //services.AddSingleton<IGorevService, GorevManager>()//istek kimden gelmis neyden gelmis bakmaz direk kullandırır ve bir kere cagrılır ve sadece bir kere cagrılmıstır 
            //services.AddTransient<IGorevService, GorevManager>();//burda ise her istekte yeni bir nesne üretir kim istemis neden istemis bakılmaz
            services.AddScoped<IGorevService, GorevManager>();//Burda direk diyorumkı burdaki ınterface cagırdıgımda direv gorev manegeri olustur burdada kullanıcı girisi yapan sessiona gore o kullanıcıya eger nesneyi üretmisse o her girdiginde o uretilen nesneyi cagırır
            services.AddScoped<IAciliyetService, AciliyetManager>();
            services.AddScoped<IRaporService, RaporManager>();//IRaporService gorddugunde RaporManegerı ornekle
            services.AddScoped<IAppUserService, AppUserManeger>();
            services.AddScoped<IDosyaService, DosyaManager>();
            services.AddScoped<IBildirimService, BildirimManager>();


            services.AddScoped<IBildirimDal, EfBildirimRepository>();
            services.AddScoped<IGorevDal, EfGorevRepository>();
            services.AddScoped<IAciliyetDal, EfAciliyetRepository>();
            services.AddScoped<IRaporDal, EfRaporRepository>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();


            services.AddTransient<ICustomLogger, NLogLogger>();
        }
    }
}
