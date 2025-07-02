using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Repositories;
using ComputerInventory.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerInventory.Data.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ComputerInventoryContext _context;

        public InventoryRepository(ComputerInventoryContext context)
        {
            _context = context;
        }
        public void Add(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Inventories.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory?> GetAsync(int id)
        {
            return await _context.Inventories.SingleOrDefaultAsync(i => i.Id == id);
        }

        public void Remove(Inventory inventory)
        {
            _context.Inventories.Remove(inventory);
        }

        public void Update(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
        }
    }
}