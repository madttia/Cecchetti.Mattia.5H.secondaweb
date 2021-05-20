using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Cecchetti.Mattia._5H.SecondaWeb.Models
{
    public class DBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;
        public DBContext()
        {
        }

        public DBContext(DbContextOptions options): base(options)
        {
            _options = options; 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=database.db");

        public DbSet<Prenotazione> Prenotazioni { get ; set; }


    }
}