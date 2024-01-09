namespace BusinessLayer.Services.AuthorBookAssociation;

public interface IAuthorBookAsssociationService : IGenericService<AuthorBookAssociationEntity, long>
{
    Task<IEnumerable<AuthorBookAssociationEntity>> CreateMultipleAssociationsAsync(BookEntity book, 
        IEnumerable<Tuple<long, bool>> associations, bool save = true);
    Task<AuthorBookAssociationEntity> FindByBookAndAuthorIdAsync(long bookId, long authorId);
}
