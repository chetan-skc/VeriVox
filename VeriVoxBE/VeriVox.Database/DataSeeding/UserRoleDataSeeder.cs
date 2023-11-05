using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Database.DataSeeding
{
    public static class UserRoleDataSeeder
    {
        public static void UserRoleDataSeed(this ModelBuilder modelBuilder)
        {
            var userRoles = new List<UserRole> 
            {
                new UserRole
                {
                    Id = 1,
                    UserId = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                    RoleId = 1,
                    CreatedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                    ModifiedDate = DateTime.UtcNow
                },
                 new UserRole
                 {
                     Id = 2,
                     UserId = Guid.Parse("8824B12B-2061-44A6-904A-413FA1BA806E"),
                     RoleId = 1,
                     CreatedBy = Guid.Parse("8824B12B-2061-44A6-904A-413FA1BA806E"),
                     CreatedDate = DateTime.UtcNow,
                     ModifiedBy = Guid.Parse("8824B12B-2061-44A6-904A-413FA1BA806E"),
                     ModifiedDate = DateTime.UtcNow
                 },
                  new UserRole
                  {
                      Id = 3,
                      UserId = Guid.Parse("26873A44-C003-47E9-A7EC-EEAC3CC23A76"),
                      RoleId = 1,
                      CreatedBy = Guid.Parse("26873A44-C003-47E9-A7EC-EEAC3CC23A76"),
                      CreatedDate = DateTime.UtcNow,
                      ModifiedBy = Guid.Parse("26873A44-C003-47E9-A7EC-EEAC3CC23A76"),
                      ModifiedDate = DateTime.UtcNow
                  },
                  new UserRole
                  {
                      Id = 4,
                      UserId = Guid.Parse("8F0B777A-3D51-4C3F-BFCB-C6F6A1CCF474"),
                      RoleId = 1,
                      CreatedBy = Guid.Parse("8F0B777A-3D51-4C3F-BFCB-C6F6A1CCF474"),
                      CreatedDate = DateTime.UtcNow,
                      ModifiedBy = Guid.Parse("8F0B777A-3D51-4C3F-BFCB-C6F6A1CCF474"),
                      ModifiedDate = DateTime.UtcNow
                  },
                  new UserRole
                  {
                      Id = 5,
                      UserId = Guid.Parse("D753378F-0D34-432F-052A-08DBC57806A8"),
                      RoleId = 2,
                      CreatedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                      CreatedDate = DateTime.UtcNow,
                      ModifiedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                      ModifiedDate = DateTime.UtcNow
                  },
                  new UserRole
                  {
                      Id = 6,
                      UserId = Guid.Parse("4FC6C89D-8050-4B98-052B-08DBC57806A8"),
                      RoleId = 3,
                      CompanyId = Guid.Parse("17D86CAE-2B96-4764-F574-08DBC57F8837"),
                      ProductId = Guid.Parse("7E72CE4F-6265-458A-3FDD-08DBC87DB756"),
                      CreatedBy = Guid.Parse("4FC6C89D-8050-4B98-052B-08DBC57806A8"),
                      CreatedDate = DateTime.UtcNow,
                      ModifiedBy = Guid.Parse("4FC6C89D-8050-4B98-052B-08DBC57806A8"),
                      ModifiedDate = DateTime.UtcNow
                  },
                  new UserRole
                  {
                      Id = 7,
                      UserId = Guid.Parse("9A8716C2-111A-4387-052C-08DBC57806A8"),
                      RoleId = 4,
                      CompanyId = Guid.Parse("17D86CAE-2B96-4764-F574-08DBC57F8837"),
                      ProductId = Guid.Parse("7E72CE4F-6265-458A-3FDD-08DBC87DB756"),
                      CreatedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                      CreatedDate = DateTime.UtcNow,
                      ModifiedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                      ModifiedDate = DateTime.UtcNow
                  },
                  new UserRole
                  {
                      Id = 8,
                      UserId = Guid.Parse("67889B9B-4DAF-4DFB-052D-08DBC57806A8"),
                      RoleId = 5,
                      CompanyId = Guid.Parse("17D86CAE-2B96-4764-F574-08DBC57F8837"),
                      ProductId = Guid.Parse("7E72CE4F-6265-458A-3FDD-08DBC87DB756"),
                      CreatedBy = Guid.Parse("67889B9B-4DAF-4DFB-052D-08DBC57806A8"),
                      CreatedDate = DateTime.UtcNow,
                      ModifiedBy = Guid.Parse("67889B9B-4DAF-4DFB-052D-08DBC57806A8"),
                      ModifiedDate = DateTime.UtcNow
                  },
                  new UserRole
                  {
                      Id = 9,
                      UserId = Guid.Parse("B2BF5561-25B7-4A99-052E-08DBC57806A8"),
                      RoleId = 6,
                      CompanyId = Guid.Parse("17D86CAE-2B96-4764-F574-08DBC57F8837"),
                      ProductId = Guid.Parse("7E72CE4F-6265-458A-3FDD-08DBC87DB756"),
                      CreatedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                      CreatedDate = DateTime.UtcNow,
                      ModifiedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                      ModifiedDate = DateTime.UtcNow
                  }

            };

            modelBuilder.Entity<UserRole>().HasData(userRoles);
        }
    }
}
