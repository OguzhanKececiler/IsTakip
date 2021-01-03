using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface IGorevDal : IGenericDal<Gorev>
    {
        List<Gorev> GetirAciliyetleTamamlanmayanlari();
        List<Gorev> GetirTumTablolarla();//Burdan olan herseyı bussiness tarafada eklemen gerekıyo
        List<Gorev> GetirTumTablolarla(Expression<Func<Gorev,bool>> filter);//Bu sekilde icerisine linq sorgusu koyabılıyoruz burda gorev ıcerısınde x=>x. dedıgımızde gorev tablosunun ozellıklerı gelır ve where sorgusu yerıne gecer
        Gorev GetirRaporlarveAppuserileID(int id);
        Gorev GetirAciliyetileId(int id);
        List<Gorev> GetirileAppUserId(int appuserId);

        List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa);

        int GetirGorevSayisiileTamamlananileAppUserId(int AppUserId);
        int GetirGorevSayisiTamamlanmasiGerekenileAppUserId(int AppUserId);

        int GetirAtanmayiBekleyenGorevSayisi();
        int GetirTamamlanmisGorevSayisi();

    }
}
