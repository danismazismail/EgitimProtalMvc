using ApplicationCore.Entities.UserEntites.Abstract;
using ApplicationCore.Entities.UserEntites.Concrete;
using DataAccess.SeedData.IdentitySeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context.IdentityContext
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        static AppIdentityDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public AppIdentityDbContext()
        {
            
        }

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserSeedData());
            builder.ApplyConfiguration(new RoleSeedData());
            builder.ApplyConfiguration(new UserRoleSeedData());

            builder.Entity<AppRole>()
            .Property(x => x.ConcurrencyStamp)
            .IsConcurrencyToken();

            builder.Entity<AppUser>(x =>
            {
                x.Property(z => z.BirthDate)
                .HasColumnType("date");
            });


            base.OnModelCreating(builder);
        }
    }
}
