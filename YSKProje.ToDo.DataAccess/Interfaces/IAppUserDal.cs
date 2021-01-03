using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
   public interface IAppUserDal/*:IGenericDal<AppUser> Generic Almama gerek yok ozel olcak cunku*/
    {
        List<AppUser> GetirAdminOlmayanlar();
        //out diyerek oraya fırlat dıyorum
        List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa,string aranacakKelime, int aktifSayfa );
        List<DualHelper> EnCokGorevTamamlamisPersoneller();
        List<DualHelper> EnCokGorevdeCalisanPersoneller();
    }
}
