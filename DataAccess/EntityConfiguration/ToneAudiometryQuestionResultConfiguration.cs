using BusinessLogic.Enums;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.EntityConfiguration;

internal class ToneAudiometryQuestionResultConfiguration : IEntityTypeConfiguration<ToneAudiometryQuestionResult>
{
    public void Configure(EntityTypeBuilder<ToneAudiometryQuestionResult> builder)
    {
        builder.ToTable("tone_audiometry_question_result");

        builder.HasKey(taqr => taqr.Id);

        builder.Property(taqr => taqr.Id)
            .HasColumnName("id")
            .HasColumnType("nvarchar(128)");

        builder.Property(taqr => taqr.Frequency)
            .HasColumnName("frequency")
            .HasColumnType("int");

        builder.Property(taqr => taqr.StartingDecibels)
            .HasColumnName("starting_decibels")
            .HasColumnType("int");

        builder.Property(taqr => taqr.LowestDecibels)
            .HasColumnName("lowest_decibels")
            .HasColumnType("int");

        builder.Property(taqr => taqr.Ear)
            .HasColumnName("ear")
            .HasColumnType("nvarchar(5)")
            .HasConversion(new EnumToStringConverter<Ear>());

        builder.Property(taqr => taqr.TestResultId)
            .HasColumnName("test_result_id")
            .HasColumnType("nvarchar(128)");
    }
}
