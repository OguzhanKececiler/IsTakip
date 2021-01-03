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

            services.AddContainerWithDepencies();//Bak bussinesda yazdýgým custonextensions eklýyorsun dependency kýsýmlarý burada olmak zorunda
        //    //Igorev servisi gordugunde gorevmanegerý asagýda belýrtýlen mimarilerden birine gore cagýr diyorum
        //    //services.AddSingleton<IGorevService, GorevManager>()//istek kimden gelmis neyden gelmis bakmaz direk kullandýrýr ve bir kere cagrýlýr ve sadece bir kere cagrýlmýstýr 
        //    //services.AddTransient<IGorevService, GorevManager>();//burda ise her istekte yeni bir nesne üretir kim istemis neden istemis bakýlmaz
        //    services.AddScoped<IGorevService, GorevManager>();//Burda direk diyorumký burdaki ýnterface cagýrdýgýmda direv gorev manegeri olustur burdada kullanýcý girisi yapan sessiona gore o kullanýcýya eger nesneyi üretmisse o her girdiginde o uretilen nesneyi cagýrýr
        //    services.AddScoped<IAciliyetService, AciliyetManager>();
        //    services.AddScoped<IRaporService,RaporManager >();//IRaporService gorddugunde RaporManegerý ornekle
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
            ////appuser ve approle kullanýcam ve entityframeforkkullanýcam onun ýcýndede todocontext db modelýmý kullanýcam
            //services.AddIdentity<AppUser, AppRole>(opt=> {
            //    opt.Password.RequireDigit = false;//sayý icerme zorunlulugunu kaldýrdým
            //    opt.Password.RequireUppercase = false;//buyuk harf icerme zorunlulugu
            //    opt.Password.RequiredLength = 1;//en az kabul edilebilir þifre karakter sayýsý
            //    opt.Password.RequireLowercase = false;//kücük harf icerme zorunlulugu
            //    opt.Password.RequireNonAlphanumeric = false;//soru isareti unlem gibi karakter isteme zorunlulugunuda false yapýyorum
            //}).AddEntityFrameworkStores<TodoContext>();

            ////kullanacagým cookie yi configure ediyorum
            //services.ConfigureApplicationCookie(opt =>
            //{
            //    opt.Cookie.Name = "IsTakipCookie";
            //    opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;//Bu cookie yi baska web sayfalaro paylasamasýn dýyoruz
            //    opt.Cookie.HttpOnly = true;//javascrpt üzerinden ulasýlmasýný istemiyorum
            //    opt.ExpireTimeSpan = TimeSpan.FromDays(20);//20 gun boyunca bu cookie kalsýn diyorum
            //    opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;//hangi tür istek gelirse ona gore davran diyorum;
            //    opt.LoginPath = "/Home/Index";
            //});


        services.AddAutoMapper(typeof(Startup));//automapperý dý(dependency ýnjection aracýlýgýyla alabýlýcem)

            //services.AddTransient<IValidator<AciliyetAddDto>, AciliyetAddValidator>();//diyorumký buradaký dto kurallarýný aciliyetaddvalidator classýndan alsýn
            //services.AddTransient<IValidator<AciliyetUpdateDto>, AciliyetUpdateValidator>();

            //services.AddTransient<IValidator<AppUserAddDto>, AppUserAddValidator>();

            //services.AddTransient<IValidator<AppUserSignInDto>,AppUserSignInValidator>();

            //services.AddTransient<IValidator<GorevAddDto>, GorevAddValidator>();
            //services.AddTransient<IValidator<GorevUpdateDto>, GorevUpdateValidator>();
            //services.AddTransient<IValidator<RaporAddDto>,RaporAddValidator>();
            //services.AddTransient<IValidator<RaporUpdateDto>,RaporUpdateValidator>();
            services.AddCustomValidator();
            services.AddControllersWithViews().AddFluentValidation();//MVC kullanýcam dýyorum validasyon yaptýgýmý soyluyorum
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<AppUser> userManager,RoleManager<AppRole>roleManager)
        {
            //yazýlým surecýndeyse
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

            }
            app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");//sayfa bulunamadý hatasý yapar
            app.UseRouting();
            //Giriþ yonetimi
            app.UseAuthentication();
            //rolbazlýyetkilendirmede yapacagýmdan
            app.UseAuthorization();
            IdentityInitializer.SeedData(userManager, roleManager).Wait();//bu sekilde kod calýsýrken bu configurelere gore calýssýn dýyorum
            app.UseStaticFiles();//wwwroot dýsarýya acýyorum

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
