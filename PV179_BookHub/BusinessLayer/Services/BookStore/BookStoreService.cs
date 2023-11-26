using BusinessLayer.Exceptions;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.BookStore
{
    public class BookStoreService : GenericService<BookStoreEntity, long>
    {
        public BookStoreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<BookStoreEntity> FindByIdAsync(long id)
        {
            var bookStore = await Repository.GetByIdAsync(id);

            if (bookStore == null)
            {
                throw new NoSuchEntityException<long>(typeof(BookStoreEntity), id);
            }

            return bookStore;
;
        }
    }
}
