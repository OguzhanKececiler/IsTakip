using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts
{
    //Icerisinde microsoftun ıcerisindeki ıdentity kuralına gore yaptıgımızdan ıdentitydbcontext dedik
    public class TodoContext : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=94.73.148.5;  database=u8417622_dbPP;  user id=u8417622_dbUPP; password=UIla66L0UIuq55D;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           //burda olusturdugumuz mapleri eklememiz gerekiyo
            modelBuilder.ApplyConfiguration(new GorevMap());
            modelBuilder.ApplyConfiguration(new RaporMap());
            modelBuilder.ApplyConfiguration(new AciliyetMap());
            modelBuilder.ApplyConfiguration(new AppUserMapping());
            modelBuilder.ApplyConfiguration(new BildirimMap());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Gorev> Gorevler { get; set; } //Burda yazılanı db nin alıp ıcerisine ekler
        public DbSet<Aciliyet> Aciliyetler { get; set; }
        public DbSet<Rapor> Raporlar { get; set; }
        public DbSet<Bildirim> Bildirimler { get; set; }


    }
}
