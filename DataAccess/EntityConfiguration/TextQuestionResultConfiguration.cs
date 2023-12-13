using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

internal class TextQuestionResultConfiguration : IEntityTypeConfiguration<TextQuestionResult>
{
    public void Configure(EntityTypeBuilder<TextQuestionResult> builder)
    {
        builder.ToTable("text_question_result");

        builder.HasKey(tqr => tqr.Id);

        builder.Property(tqr => tqr.Id)
            .HasColumnName("id")
            .HasColumnType("nvarchar(128)");

        builder.Property(tqr => tqr.Question)
            .HasColumnName("question")
            .HasColumnType("nvarchar(100)");

        builder.Property(tqr => tqr.TestResultId)
            .HasColumnName("test_result_id")
            .HasColumnType("nvarchar(128)");

        builder.HasMany(tqr => tqr.Options)
            .WithOne(tqro => tqro.TextQuestionResult)
            .HasForeignKey(tqro => tqro.TextQuestionResultId)
            .IsRequired(false);

        builder.HasMany(tqr => tqr.Answers)
            .WithOne(tqra => tqra.TextQuestionResult)
            .HasForeignKey(tqra => tqra.TextQuestionResultId);
    }
}
