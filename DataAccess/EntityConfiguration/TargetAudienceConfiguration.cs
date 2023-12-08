using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class TargetAudienceConfiguration : IEntityTypeConfiguration<TargetAudience>
{
    public void Configure(EntityTypeBuilder<TargetAudience> builder)
    {
        builder.ToTable("target_audience");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
              .HasColumnName("id")
              .HasColumnType("nvarchar(128)");
        builder.Property(t => t.From)
               .HasColumnName("from")
               .HasColumnType("int");

        builder.Property(t => t.To)
               .HasColumnName("to")
               .HasColumnType("int");

        builder.Property(t => t.Label)
               .HasColumnName("label")
               .HasColumnType("varchar(50)");

        // Define relationships
        builder.HasMany(t => t.Tests)
               .WithOne(test => test.TargetAudience)
               .HasForeignKey(test => test.TargetAudienceId);

    }
}
