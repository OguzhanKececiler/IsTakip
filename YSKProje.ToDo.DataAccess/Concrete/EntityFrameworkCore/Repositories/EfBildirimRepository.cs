using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfBildirimRepository : EfGenericRepository<Bildirim>, IBildirimDal
    {
        public int GetirOkunmayanBildirimSayisiileAppUserId(int AppUserId)
        {

            using var context = new TodoContext();
            return context.Bildirimler.Count(x => x.AppUserId == AppUserId && !x.Durum);
        }

        public List<Bildirim> GetirOkunmayanlar(int AppUserId)
        {
            using var context = new TodoContext();
            return context.Bildirimler.Where(x => x.AppUserId == AppUserId && !x.Durum).ToList();
        }
    }
}
