namespace MysticNet8App.Contracts.Exception;

public class ResourceNotFoundException : System.Exception
{
    public ResourceNotFoundException(Guid id, string resourceName)
        : base($"{resourceName} with ID {id} not found.")
    {
        Id = id;
        ResourceName = resourceName;
    }

    public Guid Id { get; }
    public string ResourceName { get; }
}