using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
   public interface IBildirimDal:IGenericDal<Bildirim>
    {
        List<Bildirim> GetirOkunmayanlar(int AppUserId);

        int GetirOkunmayanBildirimSayisiileAppUserId(int AppUserId);
    }
}
