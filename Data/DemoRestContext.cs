using L1_Zvejyba.Data.Entities;
using Microsoft.EntityFrameworkCore;

// For creating database open PowerShell and use Add-Migration
// For uptading use Update-Database



namespace L1_Zvejyba.Data
{
    public class DemoRestContext : DbContext
    {
        public DbSet<Fish> Fish { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Body> Bodies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=L1_Zvejyba");
        }


    }
}
