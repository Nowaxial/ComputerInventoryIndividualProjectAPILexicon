using ComputerInventory.Core.Common;
using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Request;

namespace ComputerInventory.Core.Repositories;

public interface IInventoryRepository
{
    Task<PagedList<Inventory>> GetAllAsync(InventoryRequestParams requestParams);
    Task<Inventory?> GetAsync(int id);
    Task<bool> AnyAsync(int id);
    void Add(Inventory inventory);
    void Update(Inventory inventory);
    void Remove(Inventory inventory);
}