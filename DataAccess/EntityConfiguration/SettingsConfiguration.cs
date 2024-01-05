using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

internal class SettingsConfiguration : IEntityTypeConfiguration<Settings>
{
    public void Configure(EntityTypeBuilder<Settings> builder)
    {
        builder.ToTable("settings");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("id")
            .HasColumnType("nvarchar(128)");

        builder.Property(x => x.Color)
            .HasColumnName("color")
            .HasColumnType("nvarchar(50)")
            .HasDefaultValue("#DA0063");

    }
}
