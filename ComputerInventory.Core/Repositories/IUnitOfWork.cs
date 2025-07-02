namespace ComputerInventory.Core.Repositories
{
    public interface IUnitOfWork
    {
        IInventoryRepository InventoryRepository { get; }
        IUserRepository UserRepository { get; }
        Task CompleteAsync();
    }
}
