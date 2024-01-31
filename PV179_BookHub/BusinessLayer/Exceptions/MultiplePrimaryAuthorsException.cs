namespace BusinessLayer.Exceptions;

public class MultiplePrimaryAuthorsException : Exception
{
    public MultiplePrimaryAuthorsException(string bookTitle) : 
        base($"Error for book with title {bookTitle}, only one primary author can be assigned to each book!")
    {
    }

    public MultiplePrimaryAuthorsException(long bookId) :
        base($"Error for book with id {bookId}, only one primary author can be assigned to each book!")
    { 
    }
}
