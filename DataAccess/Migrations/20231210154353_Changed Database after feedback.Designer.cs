﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gehoortest_application.Repository;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(Repository))]
    [Migration("20231210154353_Changed Database after feedback")]
    partial class ChangedDatabaseafterfeedback
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessLogic.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("role");

                    b.Property<string>("EmployeeNumber")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("employee_number");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("Infix")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("infix");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("last_name");

                    b.HasKey("Id");

                    b.ToTable("employee", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Models.TargetAudience", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<int>("From")
                        .HasColumnType("int")
                        .HasColumnName("from");

                    b.Property<string>("Label")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("label");

                    b.Property<int>("To")
                        .HasColumnType("int")
                        .HasColumnName("to");

                    b.HasKey("Id");

                    b.ToTable("target_audience", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Models.Test", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnName("active");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("employee_id");

                    b.Property<string>("TargetAudienceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("target_audience_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TargetAudienceId");

                    b.ToTable("test", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<bool>("HasInputField")
                        .HasColumnType("bit")
                        .HasColumnName("has_input_field");

                    b.Property<bool>("IsMultiSelect")
                        .HasColumnType("bit")
                        .HasColumnName("is_multi_select");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("question");

                    b.Property<int>("QuestionNumber")
                        .HasColumnType("int")
                        .HasColumnName("question_number");

                    b.Property<string>("TestId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("test_id");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("text_question", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestionOption", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<string>("Option")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("option");

                    b.Property<string>("TextQuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("text_question_id");

                    b.HasKey("Id");

                    b.HasIndex("TextQuestionId");

                    b.ToTable("text_question_option", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Models.ToneAudiometryQuestion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<int>("Frequency")
                        .HasColumnType("int")
                        .HasColumnName("frequency");

                    b.Property<int>("QuestionNumber")
                        .HasColumnType("int")
                        .HasColumnName("question_number");

                    b.Property<int>("StartingDecibels")
                        .HasColumnType("int")
                        .HasColumnName("starting_decibels");

                    b.Property<string>("TestId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("test_id");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("tone_audiometry_question", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Models.Test", b =>
                {
                    b.HasOne("BusinessLogic.Models.Employee", "Employee")
                        .WithMany("Tests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLogic.Models.TargetAudience", "TargetAudience")
                        .WithMany("Tests")
                        .HasForeignKey("TargetAudienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("TargetAudience");
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestion", b =>
                {
                    b.HasOne("BusinessLogic.Models.Test", "Test")
                        .WithMany("TextQuestions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestionOption", b =>
                {
                    b.HasOne("BusinessLogic.Models.TextQuestion", "TextQuestion")
                        .WithMany("Options")
                        .HasForeignKey("TextQuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TextQuestion");
                });

            modelBuilder.Entity("BusinessLogic.Models.ToneAudiometryQuestion", b =>
                {
                    b.HasOne("BusinessLogic.Models.Test", "Test")
                        .WithMany("ToneAudiometryQuestions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("BusinessLogic.Models.Employee", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("BusinessLogic.Models.TargetAudience", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("BusinessLogic.Models.Test", b =>
                {
                    b.Navigation("TextQuestions");

                    b.Navigation("ToneAudiometryQuestions");
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestion", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
