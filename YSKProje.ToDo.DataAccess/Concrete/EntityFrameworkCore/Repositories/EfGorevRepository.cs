using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGorevRepository : EfGenericRepository<Gorev>, IGorevDal
    {
        public Gorev GetirAciliyetileId(int id)
        {
            using var Context = new TodoContext();
            return Context.Gorevler.Include(x => x.Aciliyet).FirstOrDefault(x => !x.Durum && x.Id == id);
        }

        public List<Gorev> GetirAciliyetleTamamlanmayanlari()
        {
            using var Context = new TodoContext();
            return Context.Gorevler.Include(x => x.Aciliyet).Where(x => !x.Durum).OrderByDescending(x => x.OlusturulmaTarih).ToList();//Gorevler aciliyetlerle birlikte geliyor diyorum
        }

        public int GetirAtanmayiBekleyenGorevSayisi()
        {
            using var context = new TodoContext();
            return context.Gorevler.Count(x => x.AppUserId == null && !x.Durum);
        }

        public int GetirGorevSayisiileTamamlananileAppUserId(int AppUserId)
        {
            using var context = new TodoContext();
            return context.Gorevler.Count(x => x.AppUserId == AppUserId && x.Durum);
        }

        public int GetirGorevSayisiTamamlanmasiGerekenileAppUserId(int AppUserId)
        {
            using var context = new TodoContext();
            return context.Gorevler.Count(x => x.AppUserId == AppUserId && !x.Durum);
        }

        public int GetirTamamlanmisGorevSayisi()
        {

            using var context = new TodoContext();
            return context.Gorevler.Count(x=>  x.Durum);
        }

        public List<Gorev> GetirileAppUserId(int appuserId)
        {
            using var Context = new TodoContext();
            return Context.Gorevler.Where(x => x.AppUserId == appuserId).ToList();
        }


        public Gorev GetirRaporlarveAppuserileID(int id)
        {
            using var context = new TodoContext();
            return context.Gorevler.Include(x => x.Raporlar).Include(x => x.AppUser).Where(x => x.Id == id).FirstOrDefault();
        }
        public List<Gorev> GetirTumTablolarla()
        {
            using var Context = new TodoContext();
            return Context.Gorevler.Include(x => x.Aciliyet).Include(x => x.Raporlar).Include(x => x.AppUser).Where(x => !x.Durum).OrderByDescending(x => x.OlusturulmaTarih).ToList();//Gorevler aciliyetlerle birlikte geliyor diyorum
        }

        public List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter)
        {
            using var Context = new TodoContext();
            return Context.Gorevler.Include(x => x.Aciliyet).Include(x => x.Raporlar).Include(x => x.AppUser).Where(filter).OrderByDescending(x => x.OlusturulmaTarih).ToList();//Gorevler aciliyetlerle birlikte geliyor diyorum
        }

        public List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa = 1)
        {
            using var Context = new TodoContext();
            var gidecekData = Context.Gorevler.Include(x => x.Aciliyet).Include(x => x.Raporlar).Include(x => x.AppUser).Where(x => x.AppUserId == userId && x.Durum).OrderByDescending(x => x.OlusturulmaTarih);
            toplamSayfa = ((int)(Math.Ceiling((double)(gidecekData.Count() / 3))));

            return gidecekData.Skip((aktifSayfa - 1) * 3).Take(3).ToList();//boolda direk x.durum veyahut!x.durum yazılabılir.; ;//Gorevler aciliyetlerle birlikte geliyor diyorum
        }
    }
}
