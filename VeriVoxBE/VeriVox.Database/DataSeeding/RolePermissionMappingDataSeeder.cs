using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Database.DataSeeding
{
    public static class RolePermissionMappingDataSeeder
    {
        public static void RolePermissionMappingDataSeed(this ModelBuilder modelBuilder)
        {
            var rolePermissionMappings = new List<RolePermissionMapping>
            {
                new RolePermissionMapping
                {
                    Id = 1,
                    RoleId = 1,
                    PermissionId = 1,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 2,
                    RoleId = 1,
                    PermissionId = 2,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 3,
                    RoleId = 1,
                    PermissionId = 3,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 4,
                    RoleId = 1,
                    PermissionId = 4,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 5,
                    RoleId = 1,
                    PermissionId = 5,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 6,
                    RoleId = 1,
                    PermissionId = 6,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 7,
                    RoleId = 1,
                    PermissionId = 7,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 8,
                    RoleId = 1,
                    PermissionId = 8,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 9,
                    RoleId = 1,
                    PermissionId = 9,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 10,
                    RoleId = 1,
                    PermissionId = 10,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 11,
                    RoleId = 1,
                    PermissionId = 11,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 12,
                    RoleId = 2,
                    PermissionId = 1,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 13,
                    RoleId = 2,
                    PermissionId = 4,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 14,
                    RoleId = 2,
                    PermissionId = 7,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 15,
                    RoleId = 2,
                    PermissionId = 10,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 16,
                    RoleId = 2,
                    PermissionId = 11,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 17,
                    RoleId = 3,
                    PermissionId = 3,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },

                new RolePermissionMapping
                {
                    Id = 18,
                    RoleId = 3,
                    PermissionId = 4,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 19,
                    RoleId = 3,
                    PermissionId = 5,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 20,
                    RoleId = 3,
                    PermissionId = 6,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 21,
                    RoleId = 3,
                    PermissionId = 7,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 22,
                    RoleId = 3,
                    PermissionId = 8,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 23,
                    RoleId = 3,
                    PermissionId = 9,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 24,
                    RoleId = 3,
                    PermissionId = 10,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 25,
                    RoleId = 3,
                    PermissionId = 12,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 26,
                    RoleId = 4,
                    PermissionId = 4,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 27,
                    RoleId = 4,
                    PermissionId = 7,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 28,
                    RoleId = 4,
                    PermissionId = 10,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 29,
                    RoleId = 4,
                    PermissionId = 12,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 30,
                    RoleId = 5,
                    PermissionId = 4,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 31,
                    RoleId = 5,
                    PermissionId = 6,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 32,
                    RoleId = 5,
                    PermissionId = 7,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 33,
                    RoleId = 5,
                    PermissionId = 8,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 34,
                    RoleId = 5,
                    PermissionId = 9,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 35,
                    RoleId = 5,
                    PermissionId = 10,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 36,
                    RoleId = 5,
                    PermissionId = 13,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 37,
                    RoleId = 6,
                    PermissionId = 4,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 38,
                    RoleId = 6,
                    PermissionId = 7,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 39,
                    RoleId = 6,
                    PermissionId = 10,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new RolePermissionMapping
                {
                    Id = 40,
                    RoleId = 6,
                    PermissionId = 13,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                }
            };

            modelBuilder.Entity<RolePermissionMapping>().HasData(rolePermissionMappings);
        }
    }
}
