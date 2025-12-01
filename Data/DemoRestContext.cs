using L1_Zvejyba.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using L1_Zvejyba.Data.Auth.Model;
// For creating database open PowerShell and use Add-Migration
// For uptading use Update-Database

namespace L1_Zvejyba.Data
{
    public class DemoRestContext : IdentityDbContext<User>
    {
        public DbSet<Fish> Fish { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Body> Bodies { get; set; }

        public DbSet<Session> Sessions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=L1_Zvejyba");
        }


    }
}
