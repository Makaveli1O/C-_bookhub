namespace BusinessLayer.Exceptions;

public class StockErrorException : Exception
{
    public StockErrorException(uint currentStock, uint quantity)
    : base($"There is not enough stock for executing the operation! Current stock = {currentStock} and Desired quantity = {quantity}")
    {
    }
}
