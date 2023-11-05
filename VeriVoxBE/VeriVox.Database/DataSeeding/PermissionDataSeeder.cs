using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Database.DataSeeding
{
    public static class PermissionDataSeeder
    {
        public static void PermissionDataSeed(this ModelBuilder modelBuilder)
        {
            var permissions = new List<Permission>
            {
                new Permission
                {
                    Id = 1,
                    Name = "ViewAllCompanies",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 2,
                    Name = "CreateCompany",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 3,
                    Name = "EditCompany",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 4,
                    Name = "ViewCompany",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 5,
                    Name = "CreateProduct",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 6,
                    Name = "EditProduct",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 7,
                    Name = "ViewProduct",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 8,
                    Name = "CreateForm",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 9,
                    Name = "EditForm",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 10,
                    Name = "ViewForm",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 11,
                    Name = "ViewSystemReport",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 12,
                    Name = "ViewCompanyReport",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                },
                new Permission
                {
                    Id = 13,
                    Name = "ViewProductReport",
                    Description = "",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4BDDDF4266B52508DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow,
                }
            };
            modelBuilder.Entity<Permission>().HasData(permissions);
        }
    }
}
