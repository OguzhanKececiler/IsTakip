using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using YSKProje.ToDo.Business.Concrete;
using YSKProje.ToDo.Business.DiContainer;
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
using YSKProje.ToDo.Web.CustomCollectionExtensions;

namespace YSKProje.ToDo.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddContainerWithDepencies();//Bak bussinesda yazd�g�m custonextensions ekl�yorsun dependency k�s�mlar� burada olmak zorunda
        //    //Igorev servisi gordugunde gorevmaneger� asag�da bel�rt�len mimarilerden birine gore cag�r diyorum
        //    //services.AddSingleton<IGorevService, GorevManager>()//istek kimden gelmis neyden gelmis bakmaz direk kulland�r�r ve bir kere cagr�l�r ve sadece bir kere cagr�lm�st�r 
        //    //services.AddTransient<IGorevService, GorevManager>();//burda ise her istekte yeni bir nesne �retir kim istemis neden istemis bak�lmaz
        //    services.AddScoped<IGorevService, GorevManager>();//Burda direk diyorumk� burdaki �nterface cag�rd�g�mda direv gorev manegeri olustur burdada kullan�c� girisi yapan sessiona gore o kullan�c�ya eger nesneyi �retmisse o her girdiginde o uretilen nesneyi cag�r�r
        //    services.AddScoped<IAciliyetService, AciliyetManager>();
        //    services.AddScoped<IRaporService,RaporManager >();//IRaporService gorddugunde RaporManeger� ornekle
        //    services.AddScoped<IAppUserService, AppUserManeger>();
        //    services.AddScoped<IDosyaService, DosyaManager>();
        //    services.AddScoped<IBildirimService, BildirimManager>();


            //    services.AddScoped<IBildirimDal, EfBildirimRepository>();
            //    services.AddScoped<IGorevDal, EfGorevRepository>();
            //    services.AddScoped<IAciliyetDal, EfAciliyetRepository>();
            //    services.AddScoped<IRaporDal, EfRaporRepository>();
            //    services.AddScoped<IAppUserDal, EfAppUserRepository>();
            //Db ekliyorum

            services.AddDbContext<TodoContext>();
            services.AddCustomIdentity();
            ////appuser ve approle kullan�cam ve entityframeforkkullan�cam onun �c�ndede todocontext db model�m� kullan�cam
            //services.AddIdentity<AppUser, AppRole>(opt=> {
            //    opt.Password.RequireDigit = false;//say� icerme zorunlulugunu kald�rd�m
            //    opt.Password.RequireUppercase = false;//buyuk harf icerme zorunlulugu
            //    opt.Password.RequiredLength = 1;//en az kabul edilebilir �ifre karakter say�s�
            //    opt.Password.RequireLowercase = false;//k�c�k harf icerme zorunlulugu
            //    opt.Password.RequireNonAlphanumeric = false;//soru isareti unlem gibi karakter isteme zorunlulugunuda false yap�yorum
            //}).AddEntityFrameworkStores<TodoContext>();

            ////kullanacag�m cookie yi configure ediyorum
            //services.ConfigureApplicationCookie(opt =>
            //{
            //    opt.Cookie.Name = "IsTakipCookie";
            //    opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;//Bu cookie yi baska web sayfalaro paylasamas�n d�yoruz
            //    opt.Cookie.HttpOnly = true;//javascrpt �zerinden ulas�lmas�n� istemiyorum
            //    opt.ExpireTimeSpan = TimeSpan.FromDays(20);//20 gun boyunca bu cookie kals�n diyorum
            //    opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;//hangi t�r istek gelirse ona gore davran diyorum;
            //    opt.LoginPath = "/Home/Index";
            //});


        services.AddAutoMapper(typeof(Startup));//automapper� d�(dependency �njection arac�l�g�yla alab�l�cem)

            //services.AddTransient<IValidator<AciliyetAddDto>, AciliyetAddValidator>();//diyorumk� buradak� dto kurallar�n� aciliyetaddvalidator class�ndan als�n
            //services.AddTransient<IValidator<AciliyetUpdateDto>, AciliyetUpdateValidator>();

            //services.AddTransient<IValidator<AppUserAddDto>, AppUserAddValidator>();

            //services.AddTransient<IValidator<AppUserSignInDto>,AppUserSignInValidator>();

            //services.AddTransient<IValidator<GorevAddDto>, GorevAddValidator>();
            //services.AddTransient<IValidator<GorevUpdateDto>, GorevUpdateValidator>();
            //services.AddTransient<IValidator<RaporAddDto>,RaporAddValidator>();
            //services.AddTransient<IValidator<RaporUpdateDto>,RaporUpdateValidator>();
            services.AddCustomValidator();
            services.AddControllersWithViews().AddFluentValidation();//MVC kullan�cam d�yorum validasyon yapt�g�m� soyluyorum
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<AppUser> userManager,RoleManager<AppRole>roleManager)
        {
            //yaz�l�m surec�ndeyse
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

            }
            app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");//sayfa bulunamad� hatas� yapar
            app.UseRouting();
            //Giri� yonetimi
            app.UseAuthentication();
            //rolbazl�yetkilendirmede yapacag�mdan
            app.UseAuthorization();
            IdentityInitializer.SeedData(userManager, roleManager).Wait();//bu sekilde kod cal�s�rken bu configurelere gore cal�ss�n d�yorum
            app.UseStaticFiles();//wwwroot d�sar�ya ac�yorum

            app.UseEndpoints(endpoints =>
            {
                //arealar icin
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"//eger area versa exist demek

                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                  //  endpoints.MapDefaultControllerRoute();//default olarak yap diyorum
              
            });
        }
    }
}
