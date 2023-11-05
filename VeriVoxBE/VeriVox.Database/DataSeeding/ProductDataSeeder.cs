using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Database.DataSeeding
{
    public static class ProductDataSeeder
    {
        public static void ProductDataSeed(this ModelBuilder modelBuilder)
        {
            var product = new List<Products>
            {
                new Products
                {
                    Id = Guid.Parse("7E72CE4F-6265-458A-3FDD-08DBC87DB756"),
                    Name = "Evalgator",
                    Description = "For examinations",
                    Type = "Exam",
                    LogoImage = "logo",
                    ShortName = "Eval",
                    CompanyId = Guid.Parse("17D86CAE-2B96-4764-F574-08DBC57F8837"),
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = Guid.Parse("67889B9B-4DAF-4DFB-052D-08DBC57806A8"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("67889B9B-4DAF-4DFB-052D-08DBC57806A8"),
                    ModifiedDate = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Products>().HasData(product);
        }
    }
}
