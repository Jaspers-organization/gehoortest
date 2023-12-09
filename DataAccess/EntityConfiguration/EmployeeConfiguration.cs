using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employee");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
              .HasColumnName("id")
              .HasColumnType("nvarchar(128)");

        builder.Property(e => e.FirstName)
               .HasColumnName("first_name")
               .HasColumnType("nvarchar(50)");

        builder.Property(e => e.Infix)
              .HasColumnName("infix")
              .HasColumnType("nvarchar(50)");

        builder.Property(e => e.LastName)
               .HasColumnName("last_name")
               .HasColumnType("nvarchar(10)");

        builder.Property(e => e.EmployeeNumber)
               .HasColumnName("employee_number")
               .HasColumnType("nvarchar(50)");

        builder.Ignore(e => e.FullName);

        // Define relationships
        builder.HasMany(e => e.Tests)
               .WithOne(t => t.Employee)
               .HasForeignKey(t => t.EmployeeId);

    }
}
