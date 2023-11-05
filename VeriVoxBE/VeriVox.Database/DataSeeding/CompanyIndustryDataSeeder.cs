using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Database.DataSeeding
{
    public static class CompanyIndustryDataSeeder
    {

        public static void CompanyIndustrieSeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyIndustries>()
               .HasData(
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Technology" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000002"), Name = "Healthcare" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000003"), Name = "Financial Services" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000004"), Name = "Manufacturing" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000005"), Name = "RetailEnergy" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000006"), Name = "Chemicals" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000007"), Name = "Hospitality" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000008"), Name = "Education" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000009"), Name = "Agriculture" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000010"), Name = "E-commerce" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000011"), Name = "Transportation and Logistics" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000012"), Name = "Entertainment and Media" },
                   new CompanyIndustries { Id = new Guid("00000000-0000-0000-0000-000000000013"), Name = "Telecommunication" }
               );

        }

    }
}
