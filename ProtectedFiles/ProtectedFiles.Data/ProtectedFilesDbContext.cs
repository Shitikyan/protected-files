using ProtectedFiles.Data.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;

namespace ProtectedFiles.Data
{
    public class ProtectedFilesDbContext : DbContext
    {
        public ProtectedFilesDbContext() : base(ConfigurationManager.AppSettings["ConnectionString"])
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
