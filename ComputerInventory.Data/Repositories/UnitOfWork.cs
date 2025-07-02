using ComputerInventory.Core.Repositories;
using ComputerInventory.Data.Data;

namespace ComputerInventory.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ComputerInventoryContext _context;
        public IInventoryRepository InventoryRepository { get; }

        public IUserRepository UserRepository { get; }

        public UnitOfWork(ComputerInventoryContext context)
        {
            _context = context;
            InventoryRepository = new InventoryRepository(context);
            UserRepository = new UserRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}