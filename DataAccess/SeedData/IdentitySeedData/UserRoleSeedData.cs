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
    public class UserRoleSeedData : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData
                (
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse("d5c4d023-7327-4228-8a62-285b1bae7bf8"),
                        UserId = Guid.Parse("9242f3c1-d0a9-47bd-aab1-44228447ca81")
                    },
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse("68e427b9-851a-497e-a8ea-eb1d77e90a6d"),
                        UserId = Guid.Parse("994ca4eb-a2ba-4fd6-9604-ab51c7852eb1")
                    },
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse("c260905a-c63e-4ccb-8246-46808ee55975"),
                        UserId = Guid.Parse("8e2d2709-73cd-4447-b2a6-b4bee1ace17c")
                    },
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse("105a56ee-862b-4bdc-9dc2-6041128b0fb7"),
                        UserId = Guid.Parse("c79ef618-bff0-4721-b31d-edd5668ba1e3")
                    },
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse("105a56ee-862b-4bdc-9dc2-6041128b0fb7"),
                        UserId = Guid.Parse("935965f5-f54e-46a4-b1fb-323b877a640b") 
                    }
                );
        }
    }
}
