

namespace BusinessLayer.Exceptions;

public class RemoveErrorException : Exception
{
    public RemoveErrorException(Type entityToRemove, Type relType)
        : base($"Entity of type {entityToRemove} cannot be removed, " +
            $"because there are entity(s) of type {relType} attached to it!")
    {
    }
}
