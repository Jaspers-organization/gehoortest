using gehoortest.application_Repository.Models.BusinessData_Management;
using gehoortest.application_Repository.Models.LoginData_Management;
using gehoortest.application_Repository.Models.TestData_Management;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace gehoortest_application.Repository;

public class Repository : DbContext
{
    public string ConnectionString { get; set; }

    public Repository(string connectionString) => ConnectionString = connectionString;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //BusinessData-Management
        //modelBuilder.Entity<Branch>();

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

    /// <summary>
    ///  Base Get function to retrieve all the data from a table
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns>All the data from a table</returns>
    public ObservableCollection<TEntity> GetDataFromTable<TEntity>() where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();
        return new ObservableCollection<TEntity>(table.ToList());
    }

    /// <summary>
    /// Get function to retrieve data from table where condition met
    /// </summary>
    /// <typeparam name="TEntity">The table to get from</typeparam>
    /// <param name="predicate">The condition to be met</param>
    /// <returns>The data from the table</returns>
    public ObservableCollection<TEntity> GetDataFromTable<TEntity>(Func<TEntity, bool>? predicate = null) where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();
        if (predicate != null)
        {
            return new ObservableCollection<TEntity>(table.Where(predicate).ToList());
        }

        return new ObservableCollection<TEntity>(table.ToList());
    }

    /// <summary>
    /// Base Insert function to insert anything into a table
    /// </summary>
    /// <typeparam name="TEntity">The table to be inserted into</typeparam>
    /// <param name="entity">The entity to be inserted</param>
    public void Insert<TEntity>(TEntity entity) where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();
        table.Add(entity);
        SaveChanges();
    }

    /// <summary>
    /// Insert function with condition
    /// </summary>
    /// <typeparam name="TEntity">The table to be inserted into</typeparam>
    /// <param name="entity">The entity to be inserted</param>
    /// <param name="predicate">The condition to be met</param>
    public void Insert<TEntity>(TEntity entity, Func<TEntity, bool>? predicate = null) where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();

        if (predicate == null || predicate(entity))
        {
            table.Add(entity);
            SaveChanges();
        }
    }


    /// <summary>
    /// Insert statement with condition that returns a boolean
    /// </summary>
    /// <typeparam name="TEntity">The table to be inserted into</typeparam>
    /// <param name="entity">The entity to be inserted</param>
    /// <param name="predicate">The condition to be met</param>
    /// <returns>Returns if succeded or not</returns>
    public bool TryInsert<TEntity>(TEntity entity, Func<TEntity, bool>? predicate = null) where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();

        try
        {
            if (predicate == null || predicate(entity))
            {
                table.Add(entity);
                SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error during insertion: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// A function to try and update a record in the database.
    /// </summary>
    /// <typeparam name="TEntity">The table to be updated into</typeparam>
    /// <param name="updatedEntity">Object with new values</param>
    /// <param name="predicate">The condition to be met</param>
    /// <returns>Returns true or false based on if the opteration was successful</returns>
    public bool TryUpdate<TEntity>(TEntity updatedEntity, Func<TEntity, bool> predicate) where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();

        try
        {
            var entityToUpdate = table.SingleOrDefault(predicate);

            if (entityToUpdate != null)
            {                
                entityToUpdate = updatedEntity;
                SaveChanges();
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {            
            Debug.WriteLine($"Error during update: {ex.Message}");
            return false;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //turn this on to get loggin in ouput window.
        //optionsBuilder.LogTo(value => Debug.WriteLine(value), LogLevel.Trace);\
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}

