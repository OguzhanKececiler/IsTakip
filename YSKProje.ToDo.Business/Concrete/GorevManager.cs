using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
   public class GorevManager : IGorevService
    {
        private readonly IGorevDal _gorevDal;
        //Bu işlemin adı dependency injection
        public GorevManager(IGorevDal gorevDal)
        {
            _gorevDal = gorevDal;
            //efGorevRepository = new EfGorevRepository();

        }

        public Gorev GetirAciliyetileId(int id)
        {
           return  _gorevDal.GetirAciliyetileId(id);
        }

        public List<Gorev> GetirAciliyetTamamlayan()
        {
            return _gorevDal.GetirAciliyetleTamamlanmayanlari();
        }

        public int GetirAtanmayiBekleyenGorevSayisi()
        {
            return _gorevDal.GetirAtanmayiBekleyenGorevSayisi();
        }

        public int GetirGorevSayisiileTamamlananileAppUserId(int AppUserId)
        {
            return _gorevDal.GetirGorevSayisiileTamamlananileAppUserId(AppUserId);
        }

        public int GetirGorevSayisiTamamlanmasiGerekenileAppUserId(int AppUserId)
        {
            return _gorevDal.GetirGorevSayisiTamamlanmasiGerekenileAppUserId(AppUserId);
        }

        public int GetirTamamlanmisGorevSayisi()
        {
            return _gorevDal.GetirTamamlanmisGorevSayisi();
        }

        public List<Gorev> GetirHepsi()
        {
            return _gorevDal.GetirHepsi();
        }

        public Gorev GetirIdile(int id)
        {
            return _gorevDal.GetirIdile(id);
        }

        public List<Gorev> GetirileAppUserId(int appuserId)
        {
            return _gorevDal.GetirileAppUserId(appuserId);
        }

        public Gorev GetirRaporlarveAppuserileID(int id)
        {
            return _gorevDal.GetirRaporlarveAppuserileID(id);
        }

        public List<Gorev> GetirTumTablolarla()
        {
            return _gorevDal.GetirTumTablolarla();
        }

        public List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter)
        {
            return _gorevDal.GetirTumTablolarla(filter);
        }

        public List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa)
        {
            return _gorevDal.GetirTumTablolarlaTamamlanmayan(out toplamSayfa ,userId,aktifSayfa);
        }

        public void Guncelle(Gorev tablo)
        {
            _gorevDal.Guncelle(tablo);
        }

        public void Kaydet(Gorev tablo)
        {
            _gorevDal.Kaydet(tablo);
        }

        public void Sil(Gorev tablo)
        {
            _gorevDal.Sil(tablo);
        }

    }
}
