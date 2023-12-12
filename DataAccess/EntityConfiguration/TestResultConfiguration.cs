using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

internal class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
{
    public void Configure(EntityTypeBuilder<TestResult> builder)
    {
        builder.ToTable("test_result");

        builder.HasKey(tr => tr.Id);

        builder.Property(tr => tr.Id)
            .HasColumnName("id")
            .HasColumnType("nvarchar(128)");

        builder.Property(tr => tr.TargetAudience)
            .HasColumnName("target_audience")
            .HasColumnType("nvarchar(50)");

        builder.Property(tr => tr.TestDateTime)
            .HasColumnName("test_date_time")
            .HasColumnType("datetime");

        builder.Property(tr => tr.Duration)
            .HasColumnName("duration")
            .HasColumnType("int");

        builder.Property(tr => tr.HasHearingLoss)
            .HasColumnName("has_hearing_loss")
            .HasColumnType("bit");

        builder.HasMany(tr => tr.ToneAudiometryQuestions)
            .WithOne(taqr => taqr.TestResult)
            .HasForeignKey(taqr => taqr.TestResultId);

        builder.HasMany(tr => tr.TextQuestions)
            .WithOne(tqr => tqr.TestResult)
            .HasForeignKey(tqr => tqr.TestResultId);
    }
}
