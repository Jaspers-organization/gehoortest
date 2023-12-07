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
    [Migration("20231207153843_ModifiedEntityPosition")]
    partial class ModifiedEntityPosition
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.EmployeeDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmployeeNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("employee_number");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.Property<string>("Infix")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("infix");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.HasKey("Id");

                    b.ToTable("employee");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.TargetAudienceDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("From")
                        .HasColumnType("int")
                        .HasColumnName("from");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("label");

                    b.Property<int>("To")
                        .HasColumnType("int")
                        .HasColumnName("to");

                    b.HasKey("Id");

                    b.ToTable("target_audience");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.TestDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnName("active");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("employee_id");

                    b.Property<int>("TargetAudienceId")
                        .HasColumnType("int")
                        .HasColumnName("target_audience_id");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TargetAudienceId");

                    b.ToTable("test");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.TextQuestionDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("HasInputField")
                        .HasColumnType("bit")
                        .HasColumnName("has_input_field");

                    b.Property<bool>("IsMultiSelect")
                        .HasColumnType("bit")
                        .HasColumnName("is_multi_select");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("question");

                    b.Property<int>("QuestionNumber")
                        .HasColumnType("int")
                        .HasColumnName("question_number");

                    b.Property<int>("TestId")
                        .HasColumnType("int")
                        .HasColumnName("test_id");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("text_question");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.TextQuestionOptionDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Option")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("option");

                    b.Property<int>("TextQuestionId")
                        .HasColumnType("int")
                        .HasColumnName("text_question_id");

                    b.HasKey("Id");

                    b.HasIndex("TextQuestionId");

                    b.ToTable("text_question_option");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.ToneAudiometryQuestionDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Frequency")
                        .HasColumnType("int")
                        .HasColumnName("frequency");

                    b.Property<int>("QuestionNumber")
                        .HasColumnType("int")
                        .HasColumnName("question_number");

                    b.Property<int>("StartingDecibels")
                        .HasColumnType("int")
                        .HasColumnName("starting_decibels");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("tone_audiometry_question");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.TestDTO", b =>
                {
                    b.HasOne("BusinessLogic.DataTransferObjects.EmployeeDTO", "Employee")
                        .WithMany("Tests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLogic.DataTransferObjects.TargetAudienceDTO", "TargetAudience")
                        .WithMany("Tests")
                        .HasForeignKey("TargetAudienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("TargetAudience");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.TextQuestionDTO", b =>
                {
                    b.HasOne("BusinessLogic.DataTransferObjects.TestDTO", "Test")
                        .WithMany("TextQuestions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.TextQuestionOptionDTO", b =>
                {
                    b.HasOne("BusinessLogic.DataTransferObjects.TextQuestionDTO", "TextQuestion")
                        .WithMany("Options")
                        .HasForeignKey("TextQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TextQuestion");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.ToneAudiometryQuestionDTO", b =>
                {
                    b.HasOne("BusinessLogic.DataTransferObjects.TestDTO", "Test")
                        .WithMany("ToneAudiometryQuestions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.EmployeeDTO", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.TargetAudienceDTO", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.TestDTO", b =>
                {
                    b.Navigation("TextQuestions");

                    b.Navigation("ToneAudiometryQuestions");
                });

            modelBuilder.Entity("BusinessLogic.DataTransferObjects.TextQuestionDTO", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
