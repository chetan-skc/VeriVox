using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Database.DataSeeding;

namespace VeriVox.Database.Context
{
    public class CFA_DbContext : DbContext
    {
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>()
                .HasIndex(l => l.Value)
                .IsUnique();

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Created)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Permission>()
                .HasOne(p=>p.Modified)
                .WithMany()
                .HasForeignKey(p => p.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // for Role ------------------------------------------------------------------------
            modelBuilder.Entity<Role>()
                  .HasOne(p => p.Created) // Reference navigation property
                  .WithMany()
                  .HasForeignKey(p => p.CreatedBy)
                  .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
            modelBuilder.Entity<Role>()
                  .HasOne(p => p.Modified) // Reference navigation property
                  .WithMany()
                  .HasForeignKey(p => p.ModifiedBy)
                  .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
            // for UserRole ---------------------------------------------------------------------
            modelBuilder.Entity<UserRole>()
                .HasOne(p => p.Created) // Reference navigation property
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
            modelBuilder.Entity<UserRole>()
                  .HasOne(p => p.Modified) // Reference navigation property
                  .WithMany()
                  .HasForeignKey(p => p.ModifiedBy)
                  .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
            // for RolePermissionMapping --------------------------------------------------------
            modelBuilder.Entity<RolePermissionMapping>()
                .HasOne(p => p.Created) // Reference navigation property
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
            modelBuilder.Entity<RolePermissionMapping>()
                  .HasOne(p => p.Modified) // Reference navigation property
                  .WithMany()
                  .HasForeignKey(p => p.ModifiedBy)
                  .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
                                                      // for CompanyModel ---------------------------------------------------------
            modelBuilder.Entity<Companies>()
               .HasOne(p => p.Created) // Reference navigation property
               .WithMany()
               .HasForeignKey(p => p.CreatedBy)
               .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
            modelBuilder.Entity<Companies>()
                  .HasOne(p => p.Modified) // Reference navigation property
                  .WithMany()
                  .HasForeignKey(p => p.ModifiedBy)
                  .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
                                                      // for product-----------------------------------------------------------------------------

            modelBuilder.Entity<Products>()
               .HasOne(p => p.Created) // Reference navigation property
               .WithMany()
               .HasForeignKey(p => p.CreatedBy)
               .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
            modelBuilder.Entity<Products>()
                  .HasOne(p => p.Modified) // Reference navigation property
                  .WithMany()
                  .HasForeignKey(p => p.ModifiedBy)
                  .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
            base.OnModelCreating(modelBuilder);


            // for links ------------------------------------------------------------------------
            modelBuilder.Entity<Link>()
                  .HasOne(p => p.Created) // Reference navigation property
                  .WithMany()
                  .HasForeignKey(p => p.CreatedBy)
                  .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete
            modelBuilder.Entity<Link>()
                  .HasOne(p => p.Modified) // Reference navigation property
                  .WithMany()
                  .HasForeignKey(p => p.ModifiedBy)
                  .OnDelete(DeleteBehavior.Restrict); // Restrict cascade delete

            //Forms
            modelBuilder.Entity<Form>()
                .HasOne(f => f.CreatedByUser)
                .WithMany()
                .HasForeignKey(f => f.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Form>()
                .HasOne(f => f.ModifiedByUser)
                .WithMany()




                .HasForeignKey(f => f.ModifiedBy)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

            //Seeding Scopes Table
            modelBuilder.ScopeSeedData();
            //Seeding QuestionTypes Table
            modelBuilder.QuestionTypeSeedData();

            // seeding for Users table
            modelBuilder.UserDataSeed();

            // Data seeding for Role
            modelBuilder.RoleDataSeed();
            
            // Data seeding for UserRole
            modelBuilder.UserRoleDataSeed();


               // Data seeding for Permission
            modelBuilder.PermissionDataSeed();

            // Data seeding for RolePermissionMapping 
            modelBuilder.RolePermissionMappingDataSeed();

            // Data seeding for Company Industries
            modelBuilder.CompanyIndustrieSeedData();

            // Data seeding for Companies
            modelBuilder.CompanyDataSeed();

            // Data seeding for products
            modelBuilder.ProductDataSeed();
        }

        public CFA_DbContext(DbContextOptions<CFA_DbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }

        public DbSet<Scope> Scope { get; set; }
        public DbSet<Form> Form { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<FormQuestion> FormQuestion { get; set; }
        public DbSet<QuestionOption> QuestionOption { get; set; }


        public DbSet<Companies> companies { get; set; }

        public DbSet<Companies> Companies { get; set; }
        public DbSet<CompanyIndustries> CompanyIndustries { get; set; }
        public DbSet<Products> Products { get; set; }
     
        public DbSet<Link> Links { get; set; }
        
        public DbSet<Responses> Responses { get; set; }
        public DbSet<ResponseAnswers> ResponsesAnswers { get; set; }
    }
}
