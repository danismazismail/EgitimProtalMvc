using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SeedData.EntitySeedData
{
    public class TeacherSeedData : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasData
                (
                    new Teacher
                    {
                        Id = Guid.Parse("4f297294-1ec7-4692-8144-da0488492276"),
                        FirstName = "Sina Emre",
                        LastName = "Bekar",
                        BirthDate = new DateTime(1996, 01, 23),
                        Email = "snabkr7010@gmail.com",
                        AppUserID = Guid.Parse("8e2d2709-73cd-4447-b2a6-b4bee1ace17c"),
                        Course = Course.Yazilim,
                        HireDate = new DateTime(2022, 02, 02)
                    }
                );
        }
    }
}
