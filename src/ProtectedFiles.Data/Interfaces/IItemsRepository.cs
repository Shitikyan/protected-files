using ProtectedFiles.Data.Entities;
using System.Threading.Tasks;

namespace ProtectedFiles.Data.Interfaces
{
    public interface IItemsRepository
    {
        Task<Item> GetItem(int itemId);
        void AddItem(Item item);
        void UpdateItem(Item item);
        Task SaveChangesAsync();
    }
}
