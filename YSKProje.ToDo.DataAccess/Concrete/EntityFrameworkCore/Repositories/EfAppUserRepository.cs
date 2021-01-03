using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository : IAppUserDal /*EfGenericRepository<AppUser>, bunu kalitmama gerek yok*/
    {
        public List<AppUser> GetirAdminOlmayanlar()
        {
            using var Context = new TodoContext();
            //Role ismi member olan uyeler gelcek
            return Context.Users.Join(
                     Context.UserRoles,
                     user => user.Id,
                     userRole => userRole.UserId,
                     (resultUser, resultUserRole) => new
                     {
                         user = resultUser,
                         userRole = resultUserRole

                     }).Join(
                     Context.Roles,
                     twoTableResult => twoTableResult.userRole.RoleId,
                     role => role.Id,
                 (resultTable, resultRole) => new
                 {
                     user = resultTable.user,
                     userRoles = resultTable.userRole,
                     roles = resultRole

                 }).Where(x => x.roles.Name == "Member").Select(x => new AppUser
                 {
                     Id = x.user.Id,
                     Name = x.user.Name,
                     Surname = x.user.Surname,
                     Picture = x.user.Email,
                     Email = x.user.Email,
                     UserName = x.user.UserName

                 }).ToList();


        }

        public List<DualHelper> EnCokGorevTamamlamisPersoneller()
        {
            using var context = new TodoContext();
       return  context.Gorevler.Include(x => x.AppUser).Where(x => x.Durum).GroupBy(x => x.AppUser.UserName).OrderByDescending(x=>x.Count()).Take(5).Select(x=>new DualHelper{GorevSayisi=x.Count(),
          Isim=x.Key
          }).ToList();
           
        }
        public List<DualHelper> EnCokGorevdeCalisanPersoneller()
        {
            using var context = new TodoContext();
          return context.Gorevler.Include(x => x.AppUser).Where(x => !x.Durum&& x.AppUserId!=null).GroupBy(x => x.AppUser.UserName).OrderByDescending(x => x.Count()).Take(5).Select(x => new DualHelper
            {
                GorevSayisi = x.Count(),
                Isim = x.Key
            }).ToList();
           
        }


        public List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string aranacakKelime, int aktifSayfa = 1)
        {
            using var Context = new TodoContext();
            //Role ismi member olan uyeler gelcek
            var Result = Context.Users.Join(
                     Context.UserRoles,
                     user => user.Id,
                     userRole => userRole.UserId,
                     (resultUser, resultUserRole) => new
                     {
                         user = resultUser,
                         userRole = resultUserRole

                     }).Join(
                     Context.Roles,
                     twoTableResult => twoTableResult.userRole.RoleId,
                     role => role.Id,
                 (resultTable, resultRole) => new
                 {
                     user = resultTable.user,
                     userRoles = resultTable.userRole,
                     roles = resultRole

                 }).Where(x => x.roles.Name == "Member").Select(x => new AppUser
                 {
                     Id = x.user.Id,
                     Name = x.user.Name,
                     Surname = x.user.Surname,
                     Picture = x.user.Picture,
                     Email = x.user.Email,
                     UserName = x.user.UserName

                 });

            toplamSayfa = (int)Math.Ceiling((double)Result.Count() / 3);


            if (!string.IsNullOrWhiteSpace(aranacakKelime))
            {
                Result = Result.Where(x => x.Name.ToLower().Contains(aranacakKelime.ToLower()) || x.Surname.ToLower().Contains(aranacakKelime.ToLower()));
                toplamSayfa = (int)Math.Ceiling((double)Result.Count() / 3);
            }

            Result = Result.Skip((aktifSayfa - 1) * 3).Take(3);//skip o kadarını gec demek take de al demek

            return Result.ToList();

        }


    }


    //class ThreeModel { 
    //public AppUser AppUser { get; set; }
    //    public AppRole AppRole { get; set; }
    //}
}
