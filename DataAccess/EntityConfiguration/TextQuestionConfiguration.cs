using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration
{
    public class TextQuestionConfiguration : IEntityTypeConfiguration<TextQuestion>
    {
        public void Configure(EntityTypeBuilder<TextQuestion> builder)
        {
            builder.ToTable("text_question"); // Table name mapping

            builder.HasKey(tq => tq.Id); // Primary key

            builder.Property(tq => tq.Id)
               .HasColumnName("id")
               .HasColumnType("nvarchar(128)");

            builder.Property(tq => tq.Question)
                   .HasColumnName("question")
                   .HasColumnType("varchar(100)"); 

            builder.Property(tq => tq.IsMultiSelect)
                   .HasColumnName("is_multi_select")
                   .HasColumnType("bit"); 

            builder.Property(tq => tq.HasInputField)
                   .HasColumnName("has_input_field")
                   .HasColumnType("bit"); 

            builder.Property(tq => tq.QuestionNumber)
                   .HasColumnName("question_number")
                   .HasColumnType("int"); 

            builder.Property(tq => tq.TestId)
                   .HasColumnName("test_id")
                   .HasColumnType("nvarchar(128)"); 

            // Define relationships
            builder.HasOne(tq => tq.Test)
                   .WithMany(test => test.TextQuestions)
                   .HasForeignKey(tq => tq.TestId);

            builder.HasMany(tq => tq.Options)
                   .WithOne(option => option.TextQuestion)
                   .HasForeignKey(option => option.TextQuestionId);

        }
    }
}
