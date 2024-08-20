using ApplicationCore.Entities.Concrete;
using ApplicationCore.Entities.UserEntites.Concrete;
using DataAccess.SeedData.EntitySeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context.ApplicationContext
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Sizin yerinize otomatik olarak update-databsae komutunu çalışıtırır. 
            Database.Migrate();
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CustomerManager> CustomerManagers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>(x =>
            {
                x.Property(z => z.BirthDate)
                .HasColumnType("date");
                x.Property(z => z.HireDate)
                 .HasColumnType("date");
            });

            modelBuilder.Entity<Student>(x =>
            {
                x.Property(z => z.BirthDate)
                .HasColumnType("date");
            });

            modelBuilder.Entity<CustomerManager>(x =>
            {
                x.Property(z => z.BirthDate)
                .HasColumnType("date");
                x.Property(z => z.HireDate)
                .HasColumnType("date");
            });

            modelBuilder.ApplyConfiguration(new TeacherSeedData());
            modelBuilder.ApplyConfiguration(new ClassroomSeedData());
            modelBuilder.ApplyConfiguration(new StudentSeedData());
            modelBuilder.ApplyConfiguration(new CustomerManagerSeedData());

           

            base.OnModelCreating(modelBuilder);
        }
    }
}
