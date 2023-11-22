using AutoMapper;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.View;
using Infrastructure.UnitOfWork;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Services.Book;

public class BookService : BaseService, IBookService
{
    public BookService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DetailedBookViewDto> CreateBookAsync(CreateBookDto createBookDto, bool save = true)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(createBookDto.PublisherId);
        if (publisher == null )
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Publisher), createBookDto.PublisherId);
        }

        var book = _mapper.Map<DataAccessLayer.Models.Publication.Book>(createBookDto);
        book.Publisher = publisher;

        if (createBookDto.AuthorIds != null)
        {
            var authors = await _unitOfWork.AuthorRepository.GetAllAsync(author => createBookDto.AuthorIds.Contains(author.Id));

            if (authors == null || (authors.Count() != createBookDto.AuthorIds.Count()))
            {
                throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Author), createBookDto.AuthorIds);
            }
            book.Authors = authors;
        }  

        await _unitOfWork.BookRepository.AddAsync(book);
        await SaveAsync(save);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<DetailedBookViewDto> UpdateBookAsync(long id, CreateBookDto updateBookDto, bool save = true)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Book), id);
        }

        book.Title = updateBookDto.Title ?? book.Title;
        book.ISBN = updateBookDto.ISBN ?? book.ISBN;
        book.Description = updateBookDto.Description ?? book.Description;
        book.BookGenre = updateBookDto.BookGenre;
        book.Price = updateBookDto.Price;

        _unitOfWork.BookRepository.Update(book);
        await SaveAsync(save);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<List<GeneralBookViewDto>> FetchAllBooksAsync()
    {
        var books = await _unitOfWork.BookRepository.GetAllAsync();

        return _mapper.Map<List<GeneralBookViewDto>>(books);
    }

    public async Task<DetailedBookViewDto> FindBookByIdAsync(long id)
    {
        var book = await _unitOfWork.BookRepository
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
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Book), id);
        }
        _unitOfWork.BookRepository.Delete(book);
        await SaveAsync(save);
    }
}
