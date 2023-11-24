
namespace BusinessLayer.Exceptions;

public class WrongOrderStateException : Exception
{
    public WrongOrderStateException(
        DataAccessLayer.Models.Enums.OrderState oldState,
        DataAccessLayer.Models.Enums.OrderState newState)
        : base($"Cannot convert state {oldState} to {newState}!")
    {
    }
}
