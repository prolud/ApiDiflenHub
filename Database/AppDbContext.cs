using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Categorie> Categorie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=diflenhub.c744aq6g8sao.sa-east-1.rds.amazonaws.com;port=3306;database=diflenhub_dev;user=admin;password=7Perfilanonimo";

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}