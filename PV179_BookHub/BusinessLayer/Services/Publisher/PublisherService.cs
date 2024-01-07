using BusinessLayer.Exceptions;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.Publisher;

public class PublisherService : GenericService<PublisherEntity, long>
{
    public PublisherService(IUnitOfWork unitOfWork, IQuery<PublisherEntity, long> query) : base(unitOfWork, query)
    {
    }

    public override async Task<PublisherEntity> FindByIdAsync(long id)
    {
        var entity = await Repository.GetByIdAsync(id, x => x.Books);
        if (entity == null)
        {
            throw new NoSuchEntityException<long>(typeof(PublisherEntity), id);
        }
        return entity;
    }
}
