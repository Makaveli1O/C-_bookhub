namespace BusinessLayer.Exceptions;

public class WrongOrderStateException : Exception
{
    public WrongOrderStateException(
        DataAccessLayer.Models.Enums.OrderState oldState,
        DataAccessLayer.Models.Enums.OrderState newState)
        : base($"The current state <<{oldState}>> cannot be converted to <<{newState}>>!")
    {
    }

    public WrongOrderStateException(
        long id, 
        DataAccessLayer.Models.Enums.OrderState state, 
        string action) 
        : base($"Order with id <<{id}>> cannot be {action}, because it is in state <<{state}>>")
    {
    }
}
