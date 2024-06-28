namespace Shared.NotExceptions;

public class CustomerNotException: Exception
{
    public CustomerNotException(string message)
        : base(message)
    {
    }
    
    public CustomerNotException(string message, Exception inner)
        : base(message, inner)
    {
    }
}