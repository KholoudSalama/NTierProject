using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NTierProject.Shared.BaseEntities;
using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Dal.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entitytype in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entitytype.ClrType))
                {
                    var method = typeof(AppDbContext).GetMethod(
                        nameof(SoftDeleteEntities),
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                    if (method == null)
                        throw new InvalidOperationException("Could not find method 'SoftDeleteEntities'.");
                    foreach (var entity in method.GetParameters())
                    {
                        if(typeof(BaseEntity).IsAssignableFrom(entitytype.ClrType))
                        {
                            method!.MakeGenericMethod(entitytype.ClrType).Invoke(null, new object[] { builder });
                        }
                    }
                }
            }

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            var AdminRoleId = "admin-role-id";
            var adminUserId = "admin-user-id";
            var userRoleId = "user-role-id";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = AdminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
                );
            var admin = new AppUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "Admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                IsActive = true
                
            };

            var passwordHasher = new PasswordHasher<AppUser>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123");
            builder.Entity<AppUser>().HasData(admin);
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdminRoleId,
                    UserId = adminUserId
                }
                );


        }
        private static void SoftDeleteEntities<TEntity>(ModelBuilder builder) where TEntity : BaseEntity
        {
            builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedOn = DateTime.Now;
                        break;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
