using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfRaporRepository : EfGenericRepository<Rapor>, IRaporDal
    {
        public Rapor GetirGorevileID(int id)
        {
            using var context = new TodoContext();
            return context.Raporlar.Include(x => x.Gorev).ThenInclude(x=>x.Aciliyet).Where(x => x.Id == id).FirstOrDefault();
        }

        public int GetirRaporSayisi()
        {
            using var context = new TodoContext();
            return context.Raporlar.Count();
        }

        public int GetirRaporSayisiileAppUserId(int Id)
        {
            using var contex = new TodoContext();
           var result= contex.Gorevler.Include(x => x.Raporlar).Where(x => x.AppUserId == Id);
           var raporlar= result.SelectMany(x => x.Raporlar).Count();//ıcerısınde var olan tum raporları bu sekilde doner;
            return raporlar;
        }
    }
}
