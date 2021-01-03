using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class RaporMap : IEntityTypeConfiguration<Rapor>
    {
        public void Configure(EntityTypeBuilder<Rapor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Tanim).HasMaxLength(100).IsRequired();//10 karakter olsun ve sana kesin girilmeli diyorum
            builder.Property(x => x.Detay).HasColumnType("ntext");

            builder.HasOne(x => x.Gorev).WithMany(x => x.Raporlar).HasForeignKey(x => x.GorevId);
        }
    }
}
