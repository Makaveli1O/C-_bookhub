using BusinessLayer.Exceptions;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.Author;

public class AuthorService : GenericService<AuthorEntity, long>, IAuthorService
{
    public AuthorService(IUnitOfWork unitOfWork, IQuery<AuthorEntity, long> query) : base(unitOfWork, query)
    {
    }

    public async Task<IEnumerable<AuthorEntity>> FetchAllAuthorsByIdsAsync(IEnumerable<long>? ids)
    {
        if (ids == null)
        {
            return new List<AuthorEntity>();
        }

        var authors = await Repository.GetAllAsync(x => ids.Contains(x.Id));

        if (authors.Count() != ids.Count())
        {
            throw new NoSuchEntityException<long>(typeof(AuthorEntity), ids);
        }

        return authors;
    }
}
