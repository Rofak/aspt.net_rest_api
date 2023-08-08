using Microsoft.EntityFrameworkCore;
using Restfull.Entity;
using Restfull.Model;

namespace Restfull.Data
{
    public class DataContext:DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public override int SaveChanges()
        {
            AddTimeStamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimeStamps()
        {
            var entities = ChangeTracker.Entries().
                Where(x=>x.Entity is BaseEntity && (x.State==EntityState.Added || x.State==EntityState.Modified));
            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;
                if(entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).createdAt = now;
                }
                ((BaseEntity)(entity.Entity)).updatedAt = now;
            }

        }

        public DbSet<User> users { get; set; }
        public DbSet<Book> book { get; set; }
    }
}
