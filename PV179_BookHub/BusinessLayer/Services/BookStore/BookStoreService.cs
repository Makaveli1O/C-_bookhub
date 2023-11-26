using BusinessLayer.Exceptions;
using BusinessLayer.Services.Author;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.BookStore
{
    public class BookStoreService : GenericService<DataAccessLayer.Models.Logistics.BookStore, long>, IBookStoreService
    {
        public BookStoreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
