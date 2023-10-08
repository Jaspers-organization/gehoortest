using Castle.Core.Resource;
using gehoortest.application_Repository.Models.BusinessData_Management;
using gehoortest.application_Repository.Models.LoginData_Management;
using gehoortest.application_Repository.Models.TestData_Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace gehoortest_application.Repository;

public abstract class Repository : DbContext
{
    public string ConnectionString { get; set; }

    public Repository(string connectionString) => ConnectionString = connectionString;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // BusinessData-Management
        modelBuilder.Entity<Branch>();

        // LoginData-Management
        modelBuilder.Entity<Client>();

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Branch)
            .WithMany()
            .HasForeignKey(e => e.BranchId)
            .IsRequired();

        // TestData-Management
        modelBuilder.Entity<TargetAudience>();

        modelBuilder.Entity<Test>()
             .HasOne(t => t.Employee)
             .WithMany()
             .HasForeignKey(t => t.EmployeeId)
             .IsRequired();

        modelBuilder.Entity<Test>()
            .HasOne(t => t.TargetAudience)
            .WithMany()
            .HasForeignKey(t => t.TargetAudienceId)
            .IsRequired();

        modelBuilder.Entity<TestResult>()
            .HasOne(tr => tr.Branch)
            .WithMany()
            .HasForeignKey(tr => tr.BranchId)
            .IsRequired();
        
        modelBuilder.Entity<TestResult>()
            .HasOne(tr => tr.Client)
            .WithMany()
            .HasForeignKey(tr => tr.ClientId);


        base.OnModelCreating(modelBuilder);
    }


    /// <summary>
    ///  Base Get function to retrieve all the data from a table
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns>All the data from a table</returns>
    public IEnumerable<TEntity> Get<TEntity>() where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();
        return new ObservableCollection<TEntity>(table.ToList());
    }

    /// <summary>
    /// Base Get function to retrieve data from table where condition met
    /// </summary>
    /// <typeparam name="TEntity">The table to get from</typeparam>
    /// <param name="predicate">The condition to be met</param>
    /// <returns>The data from the table</returns>
    public ObservableCollection<TEntity> Get<TEntity>(Func<TEntity, bool>? predicate = null) where TEntity : class
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
    /// Base Insert function with condition
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
    /// Base Insert statement with condition that returns a boolean
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
    /// Base function to try and update a record in the database.
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
        optionsBuilder.LogTo(value => Debug.WriteLine(value), LogLevel.Trace);
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}

