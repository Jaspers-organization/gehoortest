using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("test");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
                .HasColumnName("id")
                .HasColumnType("nvarchar(128)");

        builder.Property(t => t.Title)
               .HasColumnName("title")
               .HasColumnType("varchar(50)").IsRequired();

        builder.Property(t => t.Active)
               .HasColumnName("active")
               .HasColumnType("bit").IsRequired();

        builder.Property(t => t.TargetAudienceId)
               .HasColumnName("target_audience_id")
               .HasColumnType("nvarchar(128)").IsRequired();

        builder.HasOne(t => t.TargetAudience)
               .WithMany()
               .HasForeignKey(t => t.TargetAudienceId);

        builder.Property(t => t.EmployeeId)
               .HasColumnName("employee_id")
               .HasColumnType("nvarchar(128)").IsRequired();

        builder.HasOne(t => t.Employee)
               .WithMany()
               .HasForeignKey(t => t.EmployeeId);

        builder.HasMany(t => t.TextQuestions)
               .WithOne(tq => tq.Test)
               .HasForeignKey(tq => tq.TestId);

        builder.HasMany(t => t.ToneAudiometryQuestions)
               .WithOne(tq => tq.Test)
               .HasForeignKey(tq => tq.TestId);
    }
}

