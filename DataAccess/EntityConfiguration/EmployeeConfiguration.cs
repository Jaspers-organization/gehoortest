using BusinessLogic.Enums;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.EntityConfiguration;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {

        var converter = new EnumToStringConverter<Role>();

        builder.ToTable("employee");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
              .HasColumnName("id")
              .HasColumnType("nvarchar(128)");

        builder.Property(e => e.FirstName)
               .HasColumnName("first_name")
               .HasColumnType("nvarchar(50)");

        builder.Property(e => e.AccountType)
                .HasColumnName("role")
                .HasColumnType("nvarchar(15)")
                .HasConversion(converter);

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

        builder.HasOne(e => e.EmployeeLogin)
               .WithOne(el => el.Employee)
               .HasForeignKey<EmployeeLogin>(el => el.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
