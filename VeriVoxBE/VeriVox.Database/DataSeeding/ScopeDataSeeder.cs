using Microsoft.EntityFrameworkCore;
using VeriVox.Database.DatabaseObjects;
using System.Collections.Generic;

namespace VeriVox.Database.DataSeeding
{
    public static class ScopeDataSeeder
    {
        public static void ScopeSeedData(this ModelBuilder modelBuilder)
        {
            var scopes = new List<Scope>
            {
                new Scope { Id = 1, Name = "SystemAdmin" },
                new Scope { Id = 2, Name = "SystemViewer" },
                new Scope { Id = 3, Name = "CompanyAdmin" },
                new Scope { Id = 4, Name = "CompanyViewer" },
                new Scope { Id = 5, Name = "ProductAdmin" },
                new Scope { Id = 6, Name = "ProductViewer" }
            };

            modelBuilder.Entity<Scope>().HasData(scopes);
        }
    }
}
