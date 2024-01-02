namespace BusinessLayer.Exceptions;

public class AuthorBookAssociationException : Exception
{
    public AuthorBookAssociationException(long bookId, long authorId) : base($"Book with id {bookId} " +
        $"is already assigned to author with id {authorId}")
    {
    }
}
