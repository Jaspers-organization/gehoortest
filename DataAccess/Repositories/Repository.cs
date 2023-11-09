using DataAccess.Entities;
using Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DataAccess.Repositories;

public abstract class Repository : DbContext
{
    private string ConnectionString { get; set; }

    public Repository(string connectionString) => ConnectionString = connectionString;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO: maybe abstract this to a different class? 
        modelBuilder.Entity<Branch>();

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.EmployeeLogin)
            .WithOne()
            .IsRequired();

        modelBuilder.Entity<EmployeeLogin>();

        modelBuilder.Entity<TargetAudience>();

        modelBuilder.Entity<Test>()
            .HasOne(t => t.TargetAudience)
            .WithMany()
            .HasForeignKey(t => t.TargetAudienceId)
            .IsRequired();

        modelBuilder.Entity<Test>()
            .HasMany(t => t.TextQuestions)
            .WithOne()
            .HasForeignKey(t => t.TestId)
            .IsRequired();

        modelBuilder.Entity<Test>()
            .HasMany(t => t.ToneAudiometryQuestions)
            .WithOne()
            .HasForeignKey(t => t.TestId)
            .IsRequired();

        modelBuilder.Entity<Test>()
            .HasOne(t => t.Employee)
            .WithMany()
            .HasForeignKey(t => t.EmployeeId)
            .IsRequired();

        modelBuilder.Entity<TestResult>()
            .HasOne(tr => tr.TargetAudience)
            .WithMany()
            .HasForeignKey(tr => tr.TargetAudienceId)
            .IsRequired();

        modelBuilder.Entity<TestResult>()
            .HasMany(tr => tr.TextQuestionResults)
            .WithOne()
            .HasForeignKey(t => t.TestResultId)
            .IsRequired();

        modelBuilder.Entity<TestResult>()
            .HasMany(tr => tr.TextQuestionResults)
            .WithOne()
            .HasForeignKey(t => t.TestResultId)
            .IsRequired();

        modelBuilder.Entity<TextQuestion>()
            .HasMany(t => t.TextQuestionOption)
            .WithOne()
            .HasForeignKey(t => t.TestQuestionId)
            .IsRequired();

        modelBuilder.Entity<TextQuestionAnswerResult>();

        modelBuilder.Entity<TextQuestionOption>();

        modelBuilder.Entity<TextQuestionOptionResult>();

        modelBuilder.Entity<TextQuestionResult>()
            .HasMany(t => t.TextQuestionOptionResults)
            .WithOne()
            .HasForeignKey(t => t.TestQuestionResultId)
            .IsRequired();

        modelBuilder.Entity<TextQuestionResult>()
            .HasMany(t => t.TextQuestionAnswerResults)
            .WithOne()
            .HasForeignKey(t => t.TestQuestionResultId)
            .IsRequired();

        modelBuilder.Entity<ToneAudiometryQuestion>();

        modelBuilder.Entity<ToneAudiometryQuestionResult>();

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