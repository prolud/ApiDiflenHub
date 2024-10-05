using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Categorie> Categorie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\Database\app.db");
            optionsBuilder.UseSqlite($"DataSource={dbPath};Cache=Shared");
        }
    }
}