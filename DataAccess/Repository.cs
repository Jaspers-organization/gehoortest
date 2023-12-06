using BusinessLogic.Classes;
using DataAccess.Entity.LoginData_Management;
using DataAccess.Entity.TestData_Management;
using DataAccess.Models.BusinessData_Management;
using DataAccess.Models.LoginData_Management;
using DataAccess.Models.TestData_Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace gehoortest_application.Repository;

public class Repository : DbContext
{
    public readonly string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=gehoortest;TrustServerCertificate=True;Trusted_Connection=True;";

    #region DbSets
    public virtual DbSet<TargetAudience> TargetAudiences { get; set; }
    public virtual DbSet<Test> Tests { get; set; }
    public virtual DbSet<DataAccess.Entity.TestData_Management.TextQuestion> TextQuestions { get; set; }
    public virtual DbSet<ToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }

    #endregion

    public Repository() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //// Employee - EmployeeLogin relationship (1-to-1)
        //modelBuilder.Entity<Employee>()
        //    .HasOne(e => e.EmployeeLogin)
        //    .WithOne()
        //    .HasForeignKey<EmployeeLogin>(el => el.Id);

        //// Employee - Test relationship (1-to-many)
        //modelBuilder.Entity<Employee>()
        //    .HasMany(e => e.Tests)
        //    .WithOne(t => t.Em)
        //    .HasForeignKey(t => t.);

        //// Test - TargetAudience relationship (many-to-many)
        //modelBuilder.Entity<Test>()
        //    .HasMany(t => t.TargetAudiences)
        //    .WithMany(ta => ta.Tests)
        //    .UsingEntity(j => j.ToTable("TestTargetAudience"));

        //// Test - ToneAudiometryQuestion relationship (1-to-many)
        //modelBuilder.Entity<Test>()
        //    .HasMany(t => t.ToneAudiometryQuestions)
        //    .WithOne(taq => taq.Test)
        //    .HasForeignKey(taq => taq.TestId);

        //// Test - TextQuestion relationship (1-to-many)
        //modelBuilder.Entity<Test>()
        //    .HasMany(t => t.TextQuestions)
        //    .WithOne(tq => tq.Test)
        //    .HasForeignKey(tq => tq.TestId);

        //// TextQuestion - TextQuestionOption relationship (1-to-many)
        //modelBuilder.Entity<TextQuestion>()
        //    .HasMany(tq => tq.TextQuestionOptions)
        //    .WithOne(tqo => tqo.TextQuestion)
        //    .HasForeignKey(tqo => tqo.TextQuestionId);

        //// TargetAudience - TestResult relationship (1-to-many)
        //modelBuilder.Entity<TargetAudience>()
        //    .HasMany(ta => ta.TestResults)
        //    .WithOne(tr => tr.TargetAudience)
        //    .HasForeignKey(tr => tr.TargetAudienceId);

        //// TargetAudience - Branch relationship (1-to-many)
        //modelBuilder.Entity<TargetAudience>()
        //    .HasMany(ta => ta.Branches)
        //    .WithOne(b => b.TargetAudience)
        //    .HasForeignKey(b => b.TargetAudienceId);

        //// TestResult - ToneAudiometryQuestionResult relationship (1-to-many)
        //modelBuilder.Entity<TestResult>()
        //    .HasMany(tr => tr.ToneAudiometryQuestionResults)
        //    .WithOne(taqr => taqr.TestResult)
        //    .HasForeignKey(taqr => taqr.TestResultId);

        //// TestResult - TextQuestionResult relationship (1-to-many)
        //modelBuilder.Entity<TestResult>()
        //    .HasMany(tr => tr.TextQuestionResults)
        //    .WithOne(tqr => tqr.TestResult)
        //    .HasForeignKey(tqr => tqr.TestResultId);

        //// TestQuestionResult - TextQuestionOptionResult relationship (1-to-many)
        //modelBuilder.Entity<TestQuestionResult>()
        //    .HasMany(tqr => tqr.TextQuestionOptionResults)
        //    .WithOne(tqor => tqor.TestQuestionResult)
        //    .HasForeignKey(tqor => tqor.TestQuestionResultId);

        //// TestQuestionResult - TextQuestionAnswerResult relationship (1-to-many)
        //modelBuilder.Entity<TestQuestionResult>()
        //    .HasMany(tqr => tqr.TextQuestionAnswerResults)
        //    .WithOne(tqar => tqar.TestQuestionResult)
        //    .HasForeignKey(tqar => tqar.TestQuestionResultId);


        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //turn this on/off to get loggin in ouput window.
        optionsBuilder.LogTo(value => Debug.WriteLine(value), LogLevel.Trace);
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}