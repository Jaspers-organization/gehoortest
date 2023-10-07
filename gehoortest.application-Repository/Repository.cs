using gehoortest.application_Repository.Models.BusinessData_Management;
using gehoortest.application_Repository.Models.LoginData_Management;
using gehoortest.application_Repository.Models.TestData_Management;
using Microsoft.EntityFrameworkCore;

namespace gehoortest_application.Repository;

public class Repository : DbContext
{
    public string ConnectionString { get; set; }

    public Repository(string connectionString) => ConnectionString = connectionString;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //BusinessData-Management
        modelBuilder.Entity<Branch>();

        //modelBuilder.Entity<Employee_Branch>()
        //    .HasKey(eb => new { eb.Employee_id, eb.Branch_id }); // Define the composite primary key
        //modelBuilder.Entity<Employee_Branch>()
        //    .HasOne(eb => eb.Employee_id) // Employee_Branch has one Employee
        //    .WithMany(e => e.EmployeeBranches) // Employee can have multiple Employee_Branches
        //    .HasForeignKey(eb => eb.EmployeeId); // Foreign key to Employee
        //modelBuilder.Entity<Employee_Branch>()
        //    .HasOne(eb => eb.Branch) // Employee_Branch has one Branch
        //    .WithMany(b => b.EmployeeBranches) // Branch can have multiple Employee_Branches
        //    .HasForeignKey(eb => eb.branch_id); // Foreign key to Branch

        //LoginData-Management
        modelBuilder.Entity<Client_Login>();
        modelBuilder.Entity<Employee_Login>();

        //TestData-Management
        modelBuilder.Entity<Target_Audience>();

        //modelBuilder.Entity<Test>()
        //    .HasOne(t => t.Employee) // Test has one Employee
        //    .WithMany(e => e.Tests) // Employee can have multiple Tests
        //    .HasForeignKey(t => t.EmployeeId); // Foreign key to Employee
        //modelBuilder.Entity<Test_Result>()
        //    .HasOne(tr => tr.Test) // Test_Result has one Test
        //    .WithMany(t => t.TestResults) // Test can have multiple Test_Results
        //    .HasForeignKey(tr => tr.TestId); // Foreign key to Test
        //modelBuilder.Entity<Test_Result>()
        //    .HasOne(tr => tr.Branch) // Test_Result has one Branch
        //    .WithMany(b => b.TestResults) // Branch can have multiple Test_Results
        //    .HasForeignKey(tr => tr.BranchId); // Foreign key to Branch
        //modelBuilder.Entity<Test_Result>()
        //    .HasOne(tr => tr.Client_id) // Test_Result has one Client_Login
        //    .WithMany() // Client_Login can be null (optional)
        //    .HasForeignKey(tr => tr.ClientId); // Foreign key to Client_Login


        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //turn this on to get loggin in ouput window.
        //optionsBuilder.LogTo(value => Debug.WriteLine(value), LogLevel.Trace);
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}

