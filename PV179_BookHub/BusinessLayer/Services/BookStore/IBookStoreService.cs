using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.BookStore
{
    public interface IBookStoreService : IGenericService<DataAccessLayer.Models.Logistics.BookStore, long>
    {
    }
}
