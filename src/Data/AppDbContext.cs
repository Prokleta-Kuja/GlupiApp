using Microsoft.EntityFrameworkCore;

namespace GlupiApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Person>(e =>
            {
                e.HasOne(p => p.CreatedBy).WithMany(p => p.CreatedPersons).HasForeignKey(p => p.CreatedById);
            });
        }
    }
}