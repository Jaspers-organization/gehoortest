﻿// <auto-generated />
using System;
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
    [Migration("20231223123743_add-target-audience-active-state")]
    partial class addtargetaudienceactivestate
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

            modelBuilder.Entity("BusinessLogic.Models.EmployeeLogin", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnName("active");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("employee_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("password");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("salt");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("employee_login", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Models.Settings", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<string>("Color")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("#DA0063")
                        .HasColumnName("color");

                    b.Property<int>("LoginInactiveTime")
                        .HasColumnType("int")
                        .HasColumnName("login_inactive_time");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("logo");

                    b.Property<int>("TestInactiveTime")
                        .HasColumnType("int")
                        .HasColumnName("test_inactive_time");

                    b.HasKey("Id");

                    b.ToTable("settings", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Models.TargetAudience", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnName("active");

                    b.Property<int>("From")
                        .HasColumnType("int")
                        .HasColumnName("from");

                    b.Property<string>("Label")
                        .IsRequired()
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

            modelBuilder.Entity("BusinessLogic.Models.TestResult", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<int>("Duration")
                        .HasColumnType("int")
                        .HasColumnName("duration");

                    b.Property<bool>("HasHearingLoss")
                        .HasColumnType("bit")
                        .HasColumnName("has_hearing_loss");

                    b.Property<string>("TargetAudience")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("target_audience");

                    b.Property<DateTime>("TestDateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("test_date_time");

                    b.HasKey("Id");

                    b.ToTable("test_result", (string)null);
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

            modelBuilder.Entity("BusinessLogic.Models.TextQuestionAnswerResult", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("answer");

                    b.Property<string>("TextQuestionResultId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("text_question_result_id");

                    b.HasKey("Id");

                    b.HasIndex("TextQuestionResultId");

                    b.ToTable("text_question_answer_result", (string)null);
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

            modelBuilder.Entity("BusinessLogic.Models.TextQuestionOptionResult", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<string>("Option")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("option");

                    b.Property<string>("TextQuestionResultId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("text_question_result_id");

                    b.HasKey("Id");

                    b.HasIndex("TextQuestionResultId");

                    b.ToTable("text_question_option_result", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestionResult", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("question");

                    b.Property<string>("TestResultId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("test_result_id");

                    b.HasKey("Id");

                    b.HasIndex("TestResultId");

                    b.ToTable("text_question_result", (string)null);
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

            modelBuilder.Entity("BusinessLogic.Models.ToneAudiometryQuestionResult", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("id");

                    b.Property<string>("Ear")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("ear");

                    b.Property<int>("Frequency")
                        .HasColumnType("int")
                        .HasColumnName("frequency");

                    b.Property<int>("LowestDecibels")
                        .HasColumnType("int")
                        .HasColumnName("lowest_decibels");

                    b.Property<int>("StartingDecibels")
                        .HasColumnType("int")
                        .HasColumnName("starting_decibels");

                    b.Property<string>("TestResultId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("test_result_id");

                    b.HasKey("Id");

                    b.HasIndex("TestResultId");

                    b.ToTable("tone_audiometry_question_result", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Models.EmployeeLogin", b =>
                {
                    b.HasOne("BusinessLogic.Models.Employee", "Employee")
                        .WithOne("EmployeeLogin")
                        .HasForeignKey("BusinessLogic.Models.EmployeeLogin", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
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

            modelBuilder.Entity("BusinessLogic.Models.TextQuestionAnswerResult", b =>
                {
                    b.HasOne("BusinessLogic.Models.TextQuestionResult", "TextQuestionResult")
                        .WithMany("Answers")
                        .HasForeignKey("TextQuestionResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TextQuestionResult");
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestionOption", b =>
                {
                    b.HasOne("BusinessLogic.Models.TextQuestion", "TextQuestion")
                        .WithMany("Options")
                        .HasForeignKey("TextQuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("TextQuestion");
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestionOptionResult", b =>
                {
                    b.HasOne("BusinessLogic.Models.TextQuestionResult", "TextQuestionResult")
                        .WithMany("Options")
                        .HasForeignKey("TextQuestionResultId");

                    b.Navigation("TextQuestionResult");
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestionResult", b =>
                {
                    b.HasOne("BusinessLogic.Models.TestResult", "TestResult")
                        .WithMany("TextQuestions")
                        .HasForeignKey("TestResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestResult");
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

            modelBuilder.Entity("BusinessLogic.Models.ToneAudiometryQuestionResult", b =>
                {
                    b.HasOne("BusinessLogic.Models.TestResult", "TestResult")
                        .WithMany("ToneAudiometryQuestions")
                        .HasForeignKey("TestResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestResult");
                });

            modelBuilder.Entity("BusinessLogic.Models.Employee", b =>
                {
                    b.Navigation("EmployeeLogin")
                        .IsRequired();

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

            modelBuilder.Entity("BusinessLogic.Models.TestResult", b =>
                {
                    b.Navigation("TextQuestions");

                    b.Navigation("ToneAudiometryQuestions");
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestion", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("BusinessLogic.Models.TextQuestionResult", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
