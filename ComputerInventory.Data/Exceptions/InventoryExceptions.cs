namespace ComputerInventory.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Title { get; }

        public NotFoundException(string message) : base(message)
        {
            Title = "Resource not found";
        }
    }


    public class InventoryNotFoundException : Exception
    {
        public string Title { get; }
        public InventoryNotFoundException(string message) : base(message)
        {
            Title = "Inventory not found";
        }
    }

    public class NoInventoriesFoundException : Exception
    {
        public string Title { get; }
        public NoInventoriesFoundException(string message) : base(message)
        {
            Title = "No inventories found";
        }
    }


    public class InventoryAlreadyExistsException : Exception
    {
        public string Title { get; }
        public InventoryAlreadyExistsException(string message) : base(message)
        {
            Title = "Inventory already exists";
        }
    }


    // Lägg gärna fler här: MaxUsersReachedException, ValidationException osv.
}