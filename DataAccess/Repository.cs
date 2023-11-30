using DataAccess.Models.BusinessData_Management;
using DataAccess.Models.LoginData_Management;
using DataAccess.Models.TestData_Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        //modelBuilder.Entity<Employee>()
        //    .HasOne(e => e.Branch)
        //    .WithMany()
        //    .HasForeignKey(e => e.BranchId)
        //    .IsRequired();

        // TestData-Management
        modelBuilder.Entity<TargetAudience>();

        modelBuilder.Entity<Test>();

        modelBuilder.Entity<TestResult>();

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Retrieves all data from a specified table.
    /// </summary>
    /// <typeparam name="TEntity">The table to retrieve data from.</typeparam>
    /// <returns>All data from the specified table.</returns>
    public List<TEntity> Get<TEntity>() where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();
        return new List<TEntity>(table.ToList());
    }

    /// <summary>
    /// Retrieves data from a table based on a specified condition.
    /// </summary>
    /// <typeparam name="TEntity">The table to retrieve data from.</typeparam>
    /// <param name="predicate">The condition to be met.</param>
    /// <returns>The retrieved data from the table.</returns>
    public List<TEntity> Get<TEntity>(Func<TEntity, bool>? predicate = null) where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();
        if (predicate != null)
        {
            return new List<TEntity>(table.Where(predicate).ToList());
        }

        return new List<TEntity>(table.ToList());
    }

    /// <summary>
    /// Retrieves all data from a specified table asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The table to retrieve data from.</typeparam>
    /// <returns>All data from the specified table.</returns>
    public async Task<List<TEntity>> GetAsync<TEntity>() where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();
        return await table.ToListAsync();
    }

    /// <summary>
    /// Retrieves data from a table based on a specified condition asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The table to retrieve data from.</typeparam>
    /// <param name="predicate">The condition to be met.</param>
    /// <returns>The retrieved data from the table.</returns>
    public async Task<List<TEntity>> GetAsync<TEntity>(Func<TEntity, bool>? predicate = null) where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();
        IQueryable<TEntity> query = table;

        if (predicate != null)
        {
            query = query.Where(predicate).AsQueryable();
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Inserts a record into a table.
    /// </summary>
    /// <typeparam name="TEntity">The table to insert into.</typeparam>
    /// <param name="entity">The entity to be inserted.</param>
    public void Insert<TEntity>(TEntity entity) where TEntity : class
    {
        DbSet<TEntity> table = Set<TEntity>();
        table.Add(entity);
        SaveChanges();
    }

    /// <summary>
    /// Inserts a record into a table based on a specified condition.
    /// </summary>
    /// <typeparam name="TEntity">The table to insert into.</typeparam>
    /// <param name="entity">The entity to be inserted.</param>
    /// <param name="predicate">The condition that must be met.</param>
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
    /// Inserts a record into a table based on a specified condition.
    /// </summary>
    /// <typeparam name="TEntity">The table to insert into.</typeparam>
    /// <param name="entity">The entity to be inserted.</param>
    /// <param name="predicate">An optional condition that must be met for insertion (default is null).</param>
    /// <returns>True if the operation succeeded, or false if the condition was not met or an error occurred.</returns>
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
    /// Inserts a record into a table based on a specified condition and returns a boolean indicating success.
    /// </summary>
    /// <typeparam name="TEntity">The table to insert into.</typeparam>
    /// <param name="entity">The entity to be inserted.</param>
    /// <param name="predicate">The condition to be met.</param>
    /// <returns>True if the operation succeeded; otherwise, false.</returns>
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
        //turn this on/off to get loggin in ouput window.
        optionsBuilder.LogTo(value => Debug.WriteLine(value), LogLevel.Trace);
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}