using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Database.DataSeeding
{
    public static class RoleDataSeeder
    {
        public static void RoleDataSeed(this ModelBuilder modelBuilder)
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "SystemAdmin",
                    Description = "Admin with all roles",
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Role
                {
                    Id = 2,
                    Name = "SystemViewer",
                    Description = "View Reports at System level",
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Role
                {
                    Id = 3,
                    Name = "CompanyAdmin",
                    Description = "Admin wih all roles in a company",
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Role
                {
                    Id = 4,
                    Name = "CompanyViewer",
                    Description = "View reports in a company",
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Role
                {
                    Id = 5,
                    Name = "ProductAdmin",
                    Description = "Admin wih all roles in a product",
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Role
                {
                    Id = 6,
                    Name = "ProductViewer",
                    Description = "View reports in a product",
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                }
            };

            modelBuilder.Entity<Role>().HasData(roles);
        }
    }
}
