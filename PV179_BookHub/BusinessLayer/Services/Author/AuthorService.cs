using BusinessLayer.Exceptions;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.Author;

public class AuthorService : GenericService<DataAccessLayer.Models.Publication.Author, long>, IAuthorService
{
    public AuthorService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<IEnumerable<DataAccessLayer.Models.Publication.Author>> FetchAllAuthorsByIdsAsync(IEnumerable<long>? ids)
    {
        if (ids == null)
        {
            return new List<DataAccessLayer.Models.Publication.Author>();
        }

        var authors = await Repository.GetAllAsync(x => ids.Contains(x.Id));

        if (authors.Count() != ids.Count())
        {
            throw new NoSuchEntityException<long>(typeof(DataAccessLayer.Models.Publication.Author), ids);
        }

        return authors;
    }
}
