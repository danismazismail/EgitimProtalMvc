using ApplicationCore.Entities.Concrete;
using ApplicationCore.Entities.UserEntites.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SeedData.IdentitySeedData
{
    public class UserSeedData : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();

            var admin = new AppUser
            {
                Id = Guid.Parse("9242f3c1-d0a9-47bd-aab1-44228447ca81"),
                FirstName = "Yönetici",
                LastName = "Admin",
                BirthDate = new DateTime(2000, 01, 01),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@bilgeadam.com",
                NormalizedEmail = "ADMIN@BILGEADAM.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var customerManager = new AppUser
            {
                Id = Guid.Parse("994ca4eb-a2ba-4fd6-9604-ab51c7852eb1"),
                FirstName = "Pelin",
                LastName = "Özer Serdar",
                BirthDate = new DateTime(1994, 05, 06),
                UserName = "pelin.ozerserdar",
                NormalizedUserName = "PELIN.OZERSERDAR",
                Email = "pelin.ozerserdar@bilgeadamakademi.com",
                NormalizedEmail = "PELIN.OZERSERDAR@BILGEADAMAKADEMI.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var teacher = new AppUser
            {
                Id = Guid.Parse("8e2d2709-73cd-4447-b2a6-b4bee1ace17c"),
                FirstName = "Sina Emre",
                LastName = "Bekar",
                BirthDate = new DateTime(1996, 01, 23),
                Email = "snabkr7010@gmail.com",
                NormalizedEmail = "SNABKR7010@GMAIL.COM",
                UserName = "sinaemre.bekar",
                NormalizedUserName = "SINAEMRE.BEKAR",
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var student1 = new AppUser
            {
                Id = Guid.Parse("c79ef618-bff0-4721-b31d-edd5668ba1e3"),
                FirstName = "Dicle",
                LastName = "Göya",
                BirthDate = new DateTime(1993, 04, 18),
                Email = "dicle.goya@bilgeadamakademi.com",
                NormalizedEmail = "DICLE.GOYA@BILGEADAMAKADEMI.COM",
                UserName = "dicle.goya",
                NormalizedUserName = "DICLE.GOYA",
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var student2 = new AppUser
            {
                Id = Guid.Parse("935965f5-f54e-46a4-b1fb-323b877a640b"),
                FirstName = "Ayşe Nur",
                LastName = "Cihanger",
                BirthDate = new DateTime(1997, 10, 3),
                Email = "aysenur.cihanger@bilgeadamakademi.com",
                NormalizedEmail = "AYSENUR.CIHANGER@BILGEADAMAKADEMI.COM",
                UserName = "aysenur.cihanger",
                NormalizedUserName = "AYSENUR.CIHANGER",
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            builder.HasData(admin, customerManager, teacher, student1, student2);
        }
    }
}
