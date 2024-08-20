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
    public class StudentSeedData : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData
                (
                    new Student
                    {
                        Id = Guid.Parse("db43226e-979c-403a-add3-3dbf91cde2a8"),
                        FirstName = "Dicle",
                        LastName = "Göya",
                        BirthDate = new DateTime(1993, 04, 18),
                        Email = "dicle.goya@bilgeadamakademi.com",
                        ClassroomId = Guid.Parse("a1a46a15-313c-4179-bebd-21a5aca0864c"),
                        AppUserID = Guid.Parse("c79ef618-bff0-4721-b31d-edd5668ba1e3")
                    },
                    new Student
                    {
                        Id = Guid.Parse("b978007f-24d0-4756-ae26-99d4dd8a8fd8"),
                        FirstName = "Ayşe Nur",
                        LastName = "Cihanger",
                        BirthDate = new DateTime(1997, 10, 3),
                        Email = "aysenur.cihanger@bilgeadamakademi.com",
                        ClassroomId = Guid.Parse("a1a46a15-313c-4179-bebd-21a5aca0864c"),
                        AppUserID = Guid.Parse("935965f5-f54e-46a4-b1fb-323b877a640b")
                    }
                );
        }
    }
}
