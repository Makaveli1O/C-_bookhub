using AutoMapper;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.View;
using Infrastructure.UnitOfWork;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Services.Book;

public class BookService : BaseService<DataAccessLayer.Models.Publication.Book, long>, IBookService
{
    public BookService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DetailedBookViewDto> CreateBookAsync(CreateBookDto createBookDto, bool save = true)
    {
        var book = _mapper.Map<DataAccessLayer.Models.Publication.Book>(createBookDto);
        await Repository.AddAsync(book);
        await SaveAsync(save);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<DetailedBookViewDto> UpdateBookAsync(long id, CreateBookDto updateBookDto, bool save = true)
    {
        var book = await Repository.GetByIdAsync(id);
        if (book == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Book), id);
        }

        book.Title = updateBookDto.Title ?? book.Title;
        book.ISBN = updateBookDto.ISBN ?? book.ISBN;
        book.Description = updateBookDto.Description ?? book.Description;
        book.BookGenre = updateBookDto.BookGenre;
        book.Price = updateBookDto.Price;

        Repository.Update(book);
        await SaveAsync(save);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<List<GeneralBookViewDto>> FetchAllBooksAsync()
    {
        var books = await Repository.GetAllAsync();

        return _mapper.Map<List<GeneralBookViewDto>>(books);
    }

    public async Task<DetailedBookViewDto> FindBookByIdAsync(long id)
    {
        var book = await Repository
            .GetByIdAsync(id, 
            x => x.Authors, 
            x => x.Reviews, 
            x => x.Publisher);
        if (book == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Book), id);
        }

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task DeleteBookByIdAsync(long id, bool save = true)
    {
        var book = await Repository.GetByIdAsync(id);
        if (book == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Book), id);
        }
        Repository.Delete(book);
        await SaveAsync(save);
    }
}
