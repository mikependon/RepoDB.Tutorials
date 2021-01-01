using EntityFrameworkRawSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkRawSQL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.LazyLoadingEnabled = true;
        }

        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=TestDB;Integrated Security=SSPI;");
        }
    }
}
