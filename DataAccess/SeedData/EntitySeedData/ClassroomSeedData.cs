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
    public class ClassroomSeedData : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasData
                (
                    new Classroom
                    {
                        Id = Guid.Parse("a1a46a15-313c-4179-bebd-21a5aca0864c"),
                        ClassroomName = "YZL-8150",
                        Description = "320 Saat .NET Full Stack Yazılım Uzmanlığı Eğitimi",
                        TeacherId = Guid.Parse("4f297294-1ec7-4692-8144-da0488492276")
                    }
                );
        }
    }
}
