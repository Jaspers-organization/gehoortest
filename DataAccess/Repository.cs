using BusinessLogic.Classes;
using BusinessLogic.Models;
using DataAccess.Entity.TestData_Management;
using DataAccess.Models.BusinessData_Management;
using DataAccess.Models.LoginData_Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace gehoortest_application.Repository;

public class Repository : DbContext
{
    public string ConnectionString { get; set; }

    #region DbSets
    public virtual DbSet<TargetAudience> TargetAudiences { get; set; }
    public virtual DbSet<Test> Tests { get; set; }
    public virtual DbSet<DataAccess.Entity.TestData_Management.TextQuestion> TextQuestions { get; set; }
    public virtual DbSet<ToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }

    #endregion

    public Repository(string connectionString) => ConnectionString = connectionString;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // BusinessData-Management
        modelBuilder.Entity<Branch>();

        // LoginData-Management
        modelBuilder.Entity<Client>();

        //modelBuilder.Entity<Employee>()
        //    .HasOne(e => e.Branch)
        //    .WithMany()
        //    .HasForeignKey(e => e.BranchId)
        //    .IsRequired();

        // TestData-Management
        modelBuilder.Entity<TargetAudience>();

        modelBuilder.Entity<Test>();

        //modelBuilder.Entity<TestResult>();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //turn this on/off to get loggin in ouput window.
        optionsBuilder.LogTo(value => Debug.WriteLine(value), LogLevel.Trace);
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}