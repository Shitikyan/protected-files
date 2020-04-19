using Microsoft.EntityFrameworkCore;
using ProtectedFiles.Data.Entities;
using ProtectedFiles.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProtectedFiles.Data.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly ProtectedFilesDbContext _dbContext;

        public ItemsRepository(ProtectedFilesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Item> GetItem(int itemId)
        {
            return _dbContext.Items.FirstOrDefaultAsync(q => q.ItemId == itemId);
        }

        public void AddItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            item.CreatedOn = DateTime.UtcNow;
            item.UpdatedOn = DateTime.UtcNow;

            _dbContext.Items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            item.UpdatedOn = DateTime.UtcNow;

            _dbContext.Items.Update(item);
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
