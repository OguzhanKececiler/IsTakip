using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    //Burda app userın  hanı tabloya nasıl baglandıgını bu sekilde belirtmem gerekıyo
    //Hangi kolan kac karakter alır falan filan hepsini bu sekılde gosterme işlemini yapabiliyoruz
    public class AppUserMapping : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder.Property(x => x.Name).HasMaxLength(100);//Name sadece 100 karakter olmalı 
            builder.Property(x => x.Surname).HasMaxLength(100);

            //hangi tablodan ilişkiyi belirtiyosan ondan baslamanız lazım appuserdan basladım cunku gorevler appuserda list olarak gosterdim bir tarafta belirtmen baglantıları yeterli
            //Burda diyorumki bir kullanıcının birden fazla gorevi olur bunun foreignkeyi appuserıd olarak belirtilir ve eger user silinirse AppuserID ye null degeri bas
            builder.HasMany(x => x.Gorevler).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.SetNull);
        
        }
    }
}
