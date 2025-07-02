using ComputerInventory.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerInventory.Data.Data
{
    public class ComputerInventoryContext : DbContext
    {
        public ComputerInventoryContext(DbContextOptions<ComputerInventoryContext> options)
            : base(options)
        {
        }

        public DbSet<Inventory> Inventories { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
    }
}