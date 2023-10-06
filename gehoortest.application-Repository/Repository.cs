
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using gehoortest.application_Repository.Models;

namespace gehoortest_application.Repository
{
    public class Repository : DbContext
    {
        public string ConnectionString { get; set; }
        public DbSet<Test1> Tests { get; set; }
        public Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        //"Server=(localdb)\\mssqllocaldb;Database=SchoolDb;Trusted_Connection=True;"
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }

}