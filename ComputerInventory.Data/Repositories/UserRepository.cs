using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Repositories;
using ComputerInventory.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerInventory.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ComputerInventoryContext _context;

        public UserRepository(ComputerInventoryContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public Task<bool> AnyAsync(int id)
        {
            return _context.Users.AnyAsync(u => u.Id == id);
            
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetAsync(int id, int inventoryId)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}