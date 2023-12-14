using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

internal class TextQuestionAnswerResultConfiguration : IEntityTypeConfiguration<TextQuestionAnswerResult>
{
    public void Configure(EntityTypeBuilder<TextQuestionAnswerResult> builder)
    {
        builder.ToTable("text_question_answer_result");

        builder.HasKey(tqar => tqar.Id);

        builder.Property(tqar => tqar.Id)
                .HasColumnName("id")
                .HasColumnType("nvarchar(128)");

        builder.Property(tqar => tqar.Answer)
            .HasColumnName("answer")
            .HasColumnType("nvarchar(100)");

        builder.Property(tqar => tqar.TextQuestionResultId)
            .HasColumnName("text_question_result_id")
            .HasColumnType("nvarchar(128)");
    }
}
