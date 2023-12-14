using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

internal class TextQuestionOptionResultConfiguration : IEntityTypeConfiguration<TextQuestionOptionResult>
{
    public void Configure(EntityTypeBuilder<TextQuestionOptionResult> builder)
    {
        builder.ToTable("text_question_option_result");

        builder.HasKey(tqor => tqor.Id);

        builder.Property(tqor => tqor.Id)
                .HasColumnName("id")
                .HasColumnType("nvarchar(128)");

        builder.Property(tqor => tqor.Option)
            .HasColumnName("option")
            .HasColumnType("nvarchar(50)");

        builder.Property(tqor => tqor.TextQuestionResultId)
            .HasColumnName("text_question_result_id")
            .HasColumnType("nvarchar(128)");
    }
}
