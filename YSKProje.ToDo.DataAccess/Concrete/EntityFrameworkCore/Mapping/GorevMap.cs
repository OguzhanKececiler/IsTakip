using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class GorevMap : IEntityTypeConfiguration<Gorev>
    {
        public void Configure(EntityTypeBuilder<Gorev> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();
            builder.Property(I => I.Ad).HasMaxLength(200);
            builder.Property(I => I.Aciklama).HasColumnType("ntext");

            //burda diyorumki gorevin icerisinde 1 tane aciliyet var  ama bir aciliyet durumu birden fazla gorevde olabilir bunu da görev tablosundaki aciliyetID üzerinde yapıyorum
            builder.HasOne(x => x.Aciliyet).WithMany(x => x.Gorevler).HasForeignKey(x => x.AciliyetId);
        }
    }
}
