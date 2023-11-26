using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.BookStore
{
    public class BookStoreService : GenericService<DataAccessLayer.Models.Logistics.BookStore, long>, IBookStoreService
    {
        public BookStoreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
