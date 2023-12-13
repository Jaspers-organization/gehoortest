using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

internal class EmployeeLoginConfiguration : IEntityTypeConfiguration<EmployeeLogin>
{
    public void Configure(EntityTypeBuilder<EmployeeLogin> builder)
    {
        builder.ToTable("employee_login");

        builder.HasKey(el => el.Id);

        builder.Property(el => el.Id)
              .HasColumnName("id")
              .HasColumnType("nvarchar(128)");

        builder.Property(el => el.Email)
                .HasColumnName("email")
                .HasColumnType("nvarchar(50)");

        builder.Property(el => el.Password)
                .HasColumnName("password")
                .HasColumnType("nvarchar(64)");

        builder.Property(el => el.Salt)
                .HasColumnName("salt")
                .HasColumnType("nvarchar(64)");

        builder.Property(el => el.Active)
               .HasColumnName("active")
               .HasColumnType("bit");

        builder.Property(e => e.EmployeeId)
               .HasColumnName("employee_id")
               .HasColumnType("nvarchar(128)");
    }
}
