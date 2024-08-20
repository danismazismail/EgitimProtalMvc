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
    public class CustomerManagerSeedData : IEntityTypeConfiguration<CustomerManager>
    {
        public void Configure(EntityTypeBuilder<CustomerManager> builder)
        {
            builder.HasData
                (
                    new CustomerManager
                    {
                        Id = Guid.Parse("2c662b58-f9b9-490e-ad2f-1978e4ae81ff"),
                        FirstName = "Pelin",
                        LastName = "Özer Serdar",
                        Email = "pelin.ozerserdar@bilgeadamakademi.com",
                        BirthDate = new DateTime(1994, 05, 06),
                        HireDate = new DateTime(2023, 06, 06),
                        AppUserID = Guid.Parse("994ca4eb-a2ba-4fd6-9604-ab51c7852eb1")
                    }
                );
        }
    }
}
