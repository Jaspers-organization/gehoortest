using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

internal class TextQuestionOptionConfiguration : IEntityTypeConfiguration<TextQuestionOption>
{
    public void Configure(EntityTypeBuilder<TextQuestionOption> builder)
    {
        builder.ToTable("text_question_option");

        builder.HasKey(option => option.Id);

        builder.Property(option => option.Id)
                .HasColumnName("id")
                .HasColumnType("nvarchar(128)");

        builder.Property(option => option.Option)
            .HasColumnName("option")
            .HasColumnType("varchar(50)");

        builder.Property(option => option.TextQuestionId)
            .HasColumnName("text_question_id")
            .HasColumnType("nvarchar(128)");

        builder.HasOne(option => option.TextQuestion)
            .WithMany(question => question.Options)
            .HasForeignKey(option => option.TextQuestionId)
            .IsRequired(false);
        

    }
}
