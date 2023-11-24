

namespace BusinessLayer.Services.Author;

public interface IAuthorService : IGenericService<DataAccessLayer.Models.Publication.Author, long>
{
    Task<IEnumerable<DataAccessLayer.Models.Publication.Author>> FetchAllAuthorsByIdsAsync(IEnumerable<long>? ids);
}
