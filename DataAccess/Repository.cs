using BusinessLogic.Models;
using DataAccess.EntityConfiguration;
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
    public virtual DbSet<TextQuestion> TextQuestions { get; set; }
    public virtual DbSet<ToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }//todo
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<TextQuestionOption> TextQuestionsOptions { get; set; }//todo
    public virtual DbSet<TestResult> TestResults { get; set; }
    public virtual DbSet<ToneAudiometryQuestionResult> ToneAudiometryQuestionResults { get; set; }
    public virtual DbSet<TextQuestionResult> TextQuestionResults { get; set; }
    public virtual DbSet<TextQuestionOptionResult> TextQuestionOptionResults { get; set; }
    public virtual DbSet<TextQuestionAnswerResult> TextQuestionAnswerResults { get; set; }
    #endregion

    public Repository() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TestConfiguration());
        modelBuilder.ApplyConfiguration(new TargetAudienceConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new TextQuestionConfiguration());
        modelBuilder.ApplyConfiguration(new ToneAudiometryQuestionConfiguration());
        modelBuilder.ApplyConfiguration(new TextQuestionOptionConfiguration());
        modelBuilder.ApplyConfiguration(new TestResultConfiguration());
        modelBuilder.ApplyConfiguration(new ToneAudiometryQuestionResultConfiguration());
        modelBuilder.ApplyConfiguration(new TextQuestionResultConfiguration());
        modelBuilder.ApplyConfiguration(new TextQuestionOptionResultConfiguration());
        modelBuilder.ApplyConfiguration(new TextQuestionAnswerResultConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //turn this on/off to get loggin in ouput window.
        optionsBuilder.LogTo(value => Debug.WriteLine(value), LogLevel.Trace);
        optionsBuilder.UseSqlServer(ConnectionString);
        optionsBuilder.EnableSensitiveDataLogging(true);
        optionsBuilder.EnableDetailedErrors(true);
    }
}