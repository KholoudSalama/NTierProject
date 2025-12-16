using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTierProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierProject.Dal.Configurations
{
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Code).IsRequired().HasMaxLength(50);
            builder.Property(e => e.HireDate).IsRequired();

            builder.HasOne(e => e.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(e => e.JobTitle)
                     .WithMany(j => j.Employees)
                     .HasForeignKey(e => e.JobTitleId)
                     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
