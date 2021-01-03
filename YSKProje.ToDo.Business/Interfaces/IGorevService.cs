using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Interfaces
{
    public interface IGorevService : IGenericService<Gorev>
    {
        List<Gorev> GetirAciliyetTamamlayan();
        List<Gorev> GetirTumTablolarla();//Burdan olan herseyı bussiness tarafada eklemen gerekıyo
        List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter);//Bu sekilde icerisine linq sorgusu koyabılıyoruz

        Gorev GetirAciliyetileId(int id);
        List<Gorev> GetirileAppUserId(int appuserId);
        Gorev GetirRaporlarveAppuserileID(int id);
        List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa=1);
        int GetirGorevSayisiileTamamlananileAppUserId(int AppUserId);
        int GetirGorevSayisiTamamlanmasiGerekenileAppUserId(int AppUserId);
        int GetirAtanmayiBekleyenGorevSayisi();
        int GetirTamamlanmisGorevSayisi();


    }
}
