namespace ComputerInventory.Data.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public string Title { get; }

        public UserAlreadyExistsException(string message) : base(message)
        {
            Title = "User already exists";
        }
    }

    public class NoUsersFoundException : Exception
    {
        public string Title { get; }

        public NoUsersFoundException(string message) : base(message)
        {
            Title = "No Users found";
        }
    }

    public class UserNotFoundException : Exception
    {
        public string Title { get; }
        public UserNotFoundException(string message) : base(message)
        {
            Title = "User not found";
        }
    }

    public class UserNotFoundByNameException : Exception //UserNotFoundByName
    {
        public string Title { get; }
        public UserNotFoundByNameException(string message) : base(message)
        {
            Title = "User not found by name";
        }
    }
    public class MaxUsersReachedInInventoryException : Exception
    {
        public string Title { get; }
        public MaxUsersReachedInInventoryException(string message) : base(message)
        {
            Title = "Maximum number of users reached in inventory (10)";
        }
    }
    public class UserFoundWithTheSameNameException : Exception
    {
        public string Title { get; }
        public UserFoundWithTheSameNameException(string message) : base(message)
        {
            Title = "User found with the same name";
        }
    }
}
