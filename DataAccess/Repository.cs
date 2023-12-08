using DataAccess.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace gehoortest_application.Repository;
//public static class IdentityHelpers
//{
//    public static Task EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: true);
//    public static Task DisableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: false);

//    private static Task SetIdentityInsert<T>(DbContext context, bool enable)
//    {
//        var entityType = context.Model.FindEntityType(typeof(T));
//        var value = enable ? "ON" : "OFF";
//        return context.Database.ExecuteSqlRawAsync(
//            $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
//    }

//    public static void SaveChangesWithIdentityInsert<T>(this DbContext context)
//    {
//        using var transaction = context.Database.BeginTransaction();
//        context.EnableIdentityInsert<T>();
//        context.SaveChanges();
//        context.DisableIdentityInsert<T>();
//        transaction.Commit();
//    }

//}
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
        #region Schema Definition
        modelBuilder.Entity<EmployeeDTO>().Property(e => e.Id).IsRequired();
        modelBuilder.Entity<TestDTO>().Property(e => e.Id).IsRequired();
        modelBuilder.Entity<TextQuestionDTO>().Property(e => e.Id).IsRequired();
        modelBuilder.Entity<ToneAudiometryQuestionDTO>().Property(e => e.Id).IsRequired();
        modelBuilder.Entity<TargetAudienceDTO>().Property(e => e.Id).IsRequired();
        #endregion

        #region Relations
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
        #endregion
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