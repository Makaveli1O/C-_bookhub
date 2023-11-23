using AutoMapper;
using BusinessLayer.Exceptions;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.AuthorBookAssociation;

//public class AuthorBookAssociationService : BaseService<DataAccessLayer.Models.Publication.AuthorBookAssociation, long>, IAuthorBookAssociationService
//{
//    public AuthorBookAssociationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
//    {
//    }

//    public async Task CreateAssociationsAsync(List<Tuple<long, long>> values, bool save = true)   
//    {
//        await Repository
//            .AddRangeAsync(
//                _mapper.Map<List<DataAccessLayer.Models.Publication.AuthorBookAssociation>>(values)
//                .ToArray()
//            );
//        await SaveAsync(save);
//    }

//    public async Task DeleteAssociationByIdsAsync(Tuple<long, long> ids, bool save = true)
//    {
//        var assoc = await Repository.GetSingleAsync(x => (x.AuthorId == ids.Item1) && (x.BookId == ids.Item2));
//        if (assoc == null)
//        {
//            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.AuthorBookAssociation), new List<long>() { ids.Item1, ids.Item2 });
//        }

//        Repository.Delete(assoc);
//        await SaveAsync(save);
//    }

//    public async Task UpdateAssociationByIdsAsync(Tuple<long, long> ids, long newAuthorId, bool save = true)
//    {
//        var assoc = await Repository.GetSingleAsync(x => (x.AuthorId == ids.Item1) && (x.BookId == ids.Item2));
//        if (assoc == null)
//        {
//            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.AuthorBookAssociation), new List<long>() { ids.Item1, ids.Item2 });
//        }

//        assoc.AuthorId = newAuthorId;
//        Repository.Update(assoc);
//        await SaveAsync(save);
//    }
//}
