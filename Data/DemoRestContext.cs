using L1_Zvejyba.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using L1_Zvejyba.Data.Auth.Model;

namespace L1_Zvejyba.Data
{
    public class DemoRestContext : IdentityDbContext<User>
    {
        public DemoRestContext(DbContextOptions<DemoRestContext> options)
            : base(options)
        {
        }

        public DbSet<Fish> Fish { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Body> Bodies { get; set; }
        public DbSet<Session> Sessions { get; set; }

        // Remove OnConfiguring override entirely; use Program.cs for DB config
    }
}
