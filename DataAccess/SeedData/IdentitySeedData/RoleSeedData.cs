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
    public class RoleSeedData : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            var admin = new AppRole
            {
                Id = Guid.Parse("d5c4d023-7327-4228-8a62-285b1bae7bf8"),
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            var customerMamager = new AppRole
            {
                Id = Guid.Parse("68e427b9-851a-497e-a8ea-eb1d77e90a6d"),
                Name = "customerManager",
                NormalizedName = "CUSTOMERMANAGER"
            };

            var teacher = new AppRole
            {
                Id = Guid.Parse("c260905a-c63e-4ccb-8246-46808ee55975"),
                Name = "teacher",
                NormalizedName = "TEACHER"
            };

            var student = new AppRole
            {
                Id = Guid.Parse("105a56ee-862b-4bdc-9dc2-6041128b0fb7"),
                Name = "student",
                NormalizedName = "STUDENT"
            };

            builder.HasData(admin, customerMamager, teacher, student);

        }
    }
}
