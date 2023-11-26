

namespace BusinessLayer.Services.Author;

public interface IAuthorService : IGenericService<AuthorEntity, long>
{
    Task<IEnumerable<AuthorEntity>> FetchAllAuthorsByIdsAsync(IEnumerable<long>? ids);
}
