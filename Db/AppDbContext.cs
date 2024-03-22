using ApiDiflenStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;

namespace ApiDiflenStore.Db
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString = "";
        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:AppDbContextConnectionString") ?? "";
        }

        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString)); ;
        }
    }
}
