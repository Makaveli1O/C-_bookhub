

namespace BusinessLayer.Services.AuthorBookAssociation;

public interface IAuthorBookAssociationService : IBaseService
{
    Task CreateAssociationsAsync(List<Tuple<long, long>> values, bool save = true);
    Task UpdateAssociationByIdsAsync(Tuple<long, long> ids, long newAuthorId, bool save = true);
    Task DeleteAssociationByIdsAsync(Tuple<long, long> ids, bool save = true);
}
