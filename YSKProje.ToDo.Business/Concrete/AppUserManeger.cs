using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    public class AppUserManeger : IAppUserService
    {
        IAppUserDal _userDal;

        public AppUserManeger(IAppUserDal userDal)//dependency injection
        {
            _userDal = userDal;
        }

        public List<DualHelper> EnCokGorevdeCalisanPersoneller()
        {
            return _userDal.EnCokGorevdeCalisanPersoneller();
        }

        public List<DualHelper> EnCokGorevTamamlamisPersoneller()
        {
            return _userDal.EnCokGorevTamamlamisPersoneller();
        }

        public List<AppUser> GetirAdminOlmayanlar()
        {
          return   _userDal.GetirAdminOlmayanlar();
        }

       

        public List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string aranacakKelime, int aktifSayfa=1)
        {
            return _userDal.GetirAdminOlmayanlar(out toplamSayfa, aranacakKelime, aktifSayfa);
        }
    }
}
