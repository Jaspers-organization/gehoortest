using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

internal class ToneAudiometryQuestionConfiguration : IEntityTypeConfiguration<ToneAudiometryQuestion>
{
    public void Configure(EntityTypeBuilder<ToneAudiometryQuestion> builder)
    {
        builder.ToTable("tone_audiometry_question");

        builder.HasKey(aq => aq.Id);

        builder.Property(aq => aq.Id)
               .HasColumnName("id")
               .HasColumnType("nvarchar(128)");

        builder.Property(aq => aq.QuestionNumber)
               .HasColumnName("question_number")
               .HasColumnType("int");

        builder.Property(aq => aq.Frequency)
               .HasColumnName("frequency")
               .HasColumnType("int");

        builder.Property(aq => aq.StartingDecibels)
               .HasColumnName("starting_decibels")
               .HasColumnType("int");
        
        builder.Property(aq => aq.TestId)
               .HasColumnName("test_id")
               .HasColumnType("nvarchar(128)");
        builder.Ignore(aq => aq.QuestionType);

        builder.HasOne(t => t.Test)
               .WithMany(aq => aq.ToneAudiometryQuestions)
               .HasForeignKey(tt => tt.TestId);
    }
}
