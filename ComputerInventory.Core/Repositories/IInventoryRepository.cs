using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerInventory.Core.Repositories
{
    public interface IInventoryRepository
    {
        Task<PagedList<Inventory>> GetAllAsync(InventoryRequestParams requestParams);
        Task<Inventory?> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Inventory inventory); 
        void Update(Inventory inventory);
        void Remove(Inventory inventory);
    }
}
