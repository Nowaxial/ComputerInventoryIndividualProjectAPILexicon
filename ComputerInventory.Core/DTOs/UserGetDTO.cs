namespace ComputerInventory.Core.DTOs
{
    public class UserGetDTO
    {
        public int Id { get; init; }
        public int InventoryId { get; init; }
        public string? Name { get; init; }
        public string? Position { get; init; }
    }
}