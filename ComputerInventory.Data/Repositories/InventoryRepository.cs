using ComputerInventory.Core.Common;
using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Repositories;
using ComputerInventory.Core.Request;
using ComputerInventory.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerInventory.Data.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly ComputerInventoryContext _context;

    public InventoryRepository(ComputerInventoryContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Inventory>> GetAllAsync(InventoryRequestParams requestParams)
    {
        IQueryable<Inventory> query = _context.Inventories;

        if (requestParams.IncludeUsers)
        {
            query = query.Include(i => i.Users);
        }

        return await PagedList<Inventory>.CreateAsync(query, requestParams.PageNumber, requestParams.PageSize);
    }

    public async Task<Inventory?> GetAsync(int id)
    {
        return await _context.Inventories.FindAsync(id);
    }

    public async Task<bool> AnyAsync(int id)
    {
        return await _context.Inventories.AnyAsync(i => i.Id == id);
    }

    public void Add(Inventory inventory)
    {
        _context.Inventories.Add(inventory);
    }

    public void Update(Inventory inventory)
    {
        _context.Inventories.Update(inventory);
    }

    public void Remove(Inventory inventory)
    {
        _context.Inventories.Remove(inventory);
    }
}