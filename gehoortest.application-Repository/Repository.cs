
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using gehoortest.application_Repository.Models.TestData_Management;

namespace gehoortest_application.Repository
{
    public abstract class Repository<T> : DbContext where T : class
    {
        public string ConnectionString { get; set; }
        public DbSet<T> values { get; set; }
        public Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }

}