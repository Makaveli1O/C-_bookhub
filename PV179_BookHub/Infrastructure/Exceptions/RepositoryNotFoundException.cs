

namespace Infrastructure.Exceptions;

public class RepositoryNotFoundException : Exception
{
    public RepositoryNotFoundException(Type type) : base($"Entity of type <<{type}>>, was not found.")
    {
        EntityType = type;
    }
    public Type EntityType { get; }
}
