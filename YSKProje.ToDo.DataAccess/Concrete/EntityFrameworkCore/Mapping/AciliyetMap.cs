using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class AciliyetMap : IEntityTypeConfiguration<Aciliyet>//bu aciliyet tablosunun mappingi diyorum
    {
        public void Configure(EntityTypeBuilder<Aciliyet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(I => I.Id).UseIdentityColumn();
            builder.Property(x => x.Tanim).HasMaxLength(100);
        }
    }
}
