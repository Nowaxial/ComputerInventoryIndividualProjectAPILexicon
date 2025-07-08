using ComputerInventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerInventory.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        Task <User?> GetByNameAsync(string name);
        void Add(User user);
        void Update(User user);
        void Remove(User user);
    }
}
