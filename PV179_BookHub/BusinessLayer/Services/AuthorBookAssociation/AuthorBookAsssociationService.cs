using BusinessLayer.Exceptions;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.AuthorBookAssociation;

public class AuthorBookAsssociationService : GenericService<AuthorBookAssociationEntity, long>, IAuthorBookAsssociationService
{
    public AuthorBookAsssociationService(IUnitOfWork unitOfWork, IQuery<AuthorBookAssociationEntity, long> query) : base(unitOfWork, query)
    {
    }

    public async Task<IEnumerable<AuthorBookAssociationEntity>> CreateMultipleAssociationsAsync(BookEntity book, IEnumerable<Tuple<long, bool>> associations, bool save = true)
    {
        var result = new List<AuthorBookAssociationEntity>();

        foreach (var association in associations)
        {
            result.Add(
                new AuthorBookAssociationEntity
                {
                    Book = book,
                    AuthorId = association.Item1,
                    IsPrimary = association.Item2
                });
        }

        await Repository.AddRangeAsync(result.ToArray());

        await SaveAsync(save);

        return result;
    }

    public async Task<AuthorBookAssociationEntity> FindByBookAndAuthorIdAsync(long bookId, long authorId)
    {
        var result = await Repository
            .GetSingleAsync(assoc => assoc.BookId == bookId && assoc.AuthorId == authorId);

        if (result == null)
        {
            throw new NoSuchEntityException<long>(typeof(AuthorBookAssociationEntity));
        }

        return result;
    }
}
