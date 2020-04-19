using Microsoft.EntityFrameworkCore;
using ProtectedFiles.Data.Entities;

namespace ProtectedFiles.Data
{
    public class ProtectedFilesDbContext : DbContext
    {
        public ProtectedFilesDbContext(DbContextOptions<ProtectedFilesDbContext> options) : base(options)
        { 
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
