using BusinessLogic.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace gehoortest_application.Repository;

public class Repository : DbContext
{
    public readonly string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=gehoortest;TrustServerCertificate=True;Trusted_Connection=True;";

    #region DbSets
    public virtual DbSet<TargetAudienceDTO> TargetAudiences { get; set; }
    public virtual DbSet<TestDTO> Tests { get; set; }
    public virtual DbSet<TextQuestionDTO> TextQuestions { get; set; }
    public virtual DbSet<ToneAudiometryQuestionDTO> ToneAudiometryQuestions { get; set; }
    public virtual DbSet<EmployeeDTO> Employees { get; set; }
    #endregion

    public Repository() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDTO>()
            .HasMany(e => e.Tests)
            .WithOne(t => t.Employee)
            .HasForeignKey(t => t.EmployeeId);

        modelBuilder.Entity<TestDTO>()
            .HasOne(t => t.TargetAudience)
            .WithMany(ta => ta.Tests)
            .HasForeignKey(t => t.TargetAudienceId);

        modelBuilder.Entity<TestDTO>()
            .HasMany(t => t.TextQuestions)
            .WithOne(tq => tq.Test)
            .HasForeignKey(tq => tq.TestId);

        modelBuilder.Entity<TestDTO>()
            .HasMany(t => t.ToneAudiometryQuestions)
            .WithOne(tq => tq.Test)
            .HasForeignKey(tq => tq.TestId);

        modelBuilder.Entity<TextQuestionDTO>()
            .HasMany(tq => tq.Options)
            .WithOne(o => o.TextQuestion)
            .HasForeignKey(o => o.TextQuestionId);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //turn this on/off to get loggin in ouput window.
        optionsBuilder.LogTo(value => Debug.WriteLine(value), LogLevel.Trace);
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}