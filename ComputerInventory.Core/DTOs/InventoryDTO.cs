using ComputerInventory.Core.Entities;

namespace ComputerInventory.Core.DTOs
{
    public class InventoryDTO
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public DateTime StartDateForCheckingInventory { get; init; }
        public DateTime EndDateForCheckingInventory => StartDateForCheckingInventory.AddMonths(3);
        public string? City { get; init; }
        public ICollection<User>? Users { get; init; }
    }
}