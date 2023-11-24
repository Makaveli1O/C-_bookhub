using BusinessLayer.Exceptions;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.Publisher;

public class PublisherService : GenericService<DataAccessLayer.Models.Publication.Publisher, long>
{
    public PublisherService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<DataAccessLayer.Models.Publication.Publisher> FindByIdAsync(long id)
    {
        var entity = await Repository.GetByIdAsync(id, x => x.Books);
        if (entity == null)
        {
            throw new NoSuchEntityException<long>(typeof(DataAccessLayer.Models.Publication.Publisher), id);
        }
        return entity;
    }
}
