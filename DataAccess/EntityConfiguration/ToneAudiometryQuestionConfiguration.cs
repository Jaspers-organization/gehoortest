using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

internal class ToneAudiometryQuestionConfiguration : IEntityTypeConfiguration<ToneAudiometryQuestion>
{
    public void Configure(EntityTypeBuilder<ToneAudiometryQuestion> builder)
    {
        builder.ToTable("tone_audiometry_question");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Id)
               .HasColumnName("id")
               .HasColumnType("nvarchar(128)");

        builder.Property(q => q.Frequency)
               .HasColumnName("frequency")
               .HasColumnType("int");

        builder.Property(q => q.StartingDecibels)
               .HasColumnName("starting_decibels")
               .HasColumnType("int");

        builder.Property(q => q.QuestionNumber)
               .HasColumnName("question_number")
               .HasColumnType("int");

        builder.Property(q => q.TestId)
               .HasColumnName("test_id")
               .HasColumnType("nvarchar(128)");

        builder.HasOne(q => q.Test)
               .WithMany(t => t.ToneAudiometryQuestions)
               .HasForeignKey(q => q.TestId);
    }
}
