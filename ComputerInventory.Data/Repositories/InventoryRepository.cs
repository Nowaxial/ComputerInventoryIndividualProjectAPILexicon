using Bogus.DataSets;
using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Repositories;
using ComputerInventory.Core.Request;
using ComputerInventory.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ComputerInventory.Data.Repositories
{
    public class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
    {
        public InventoryRepository(ComputerInventoryContext context) : base(context)
        {
        }
        public void Add(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Inventories.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync(bool includeUsers = false)
        {
            var query = _context.Inventories.AsQueryable();
            if (includeUsers)
            {
                query = query.Include(i => i.Users);
            }
            return await query.ToListAsync();
        }

        public async Task<Inventory?> GetAsync(int id)
        {
            return await _context.Inventories.FindAsync(id);
        }

        //public Task<PagedList<Inventory>> GetAllAsync(InventoryRequestParams requestParams)
        //{
        //    var inventories = requestParams.IncludeUsers ? FindAll().Include(i => i.Users)
        //                                                 : FindAll();
        //    return PagedList<Inventory>.CreateAsync(inventories, requestParams.PageNumber, requestParams.PageSize);
        //}

        public async Task<PagedList<Inventory>> GetAllAsync(InventoryRequestParams requestParams)
        {
            //var inventories = requestParams.IncludeUsers ? FindAll().Include(i => i.Users)
            //                                             : FindAll();
            //return  await PagedList<Inventory>.CreateAsync(inventories, requestParams.PageNumber, requestParams.PageSize);

            IQueryable<Inventory> query = FindAll();

            query = requestParams.IncludeUsers ? query.Include(i => i.Users) : query;

            return await PagedList<Inventory>.CreateAsync(query, requestParams.PageNumber, requestParams.PageSize);

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