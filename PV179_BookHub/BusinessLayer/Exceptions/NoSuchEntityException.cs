

namespace BusinessLayer.Exceptions;

public class NoSuchEntityException : Exception
{
    public NoSuchEntityException(Type type, long id) 
        : base($"Entity {type.Name} with given id ({id}) does not exist!")
    {
    }

    public NoSuchEntityException(Type type, IEnumerable<long> ids)
        : base($"Entities {type.Name} with ids ({ids.ToString()}) do not exist!")
    {
    }
}
