using AutoMapper;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.Filter;
using BusinessLayer.DTOs.Book.Update;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using BusinessLayer.Services.Author;
using BusinessLayer.Services.AuthorBookAssociation;
using Infrastructure.Query;
using Infrastructure.Query.Filters.EntityFilters;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Facades.Book;

public class BookFacade : BaseFacade, IBookFacade
{
    private readonly IGenericService<BookEntity, long> _bookService;
    private readonly IAuthorService _authorService;
    private readonly IGenericService<PublisherEntity, long> _publisherService;
    private readonly IAuthorBookAsssociationService _authorBookAsssociationService;

    public BookFacade(IMapper mapper, IGenericService<BookEntity, long> bookService, IAuthorService authorService, 
        IGenericService<PublisherEntity, long> publisherService, IAuthorBookAsssociationService authorBookAsssociationService,
         IMemoryCache memoryCache)
        : base(mapper, memoryCache, "book-")
    {
        _bookService = bookService;
        _authorService = authorService;
        _publisherService = publisherService;
        _authorBookAsssociationService = authorBookAsssociationService;
    }

    private static void CheckForMultiplePrimaryAuthors(string title, IEnumerable<AuthorAssocDto>? authorAssocDto)
    {
        if (authorAssocDto?.Count(author => author.IsPrimary) > 1)
        {
            throw new MultiplePrimaryAuthorsException(title);
        }
    }

    private static IEnumerable<Tuple<long, bool>> ConvertAuthorAssociationDto(IEnumerable<AuthorAssocDto>? authorAssocDto)
    {
        return authorAssocDto?
            .Select(author => new Tuple<long, bool>(author.Id, author.IsPrimary)) 
            ?? new List<Tuple<long, bool>>();
    }

    public async Task<DetailedBookViewDto> CreateBookAsync(CreateBookDto createBookDto)
    {
        CheckForMultiplePrimaryAuthors(createBookDto.Title, createBookDto.AuthorIds);

        var publisher = await _publisherService.FindByIdAsync(createBookDto.PublisherId);
        var authors = await _authorService
            .FetchAllAuthorsByIdsAsync(createBookDto.AuthorIds?.Select(author => author.Id));

        var assocTuple = ConvertAuthorAssociationDto(createBookDto.AuthorIds);

        var book = _mapper.Map<BookEntity>(createBookDto);
        book.Publisher = publisher;
        book.AuthorBookAssociations = await _authorBookAsssociationService
            .CreateMultipleAssociationsAsync(book, assocTuple, false);

        book = await _bookService.CreateAsync(book);
        book.Authors = authors;

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<DetailedBookViewDto> UpdateBookAsync(long id, UpdateBookDto updateBookDto)
    {
        var book = await _bookService.FindByIdAsync(id);

        if (updateBookDto.PublisherId != null 
            && updateBookDto.PublisherId != book.PublisherId)
        {
            book.PublisherId = (long)updateBookDto.PublisherId;
            book.Publisher = null;
        }

        book.Title = updateBookDto.Title ?? book.Title;
        book.ISBN = updateBookDto.ISBN ?? book.ISBN;
        book.BookGenre = updateBookDto.BookGenre ?? book.BookGenre;
        book.Description = updateBookDto.Description ?? book.Description;
        book.Price = updateBookDto.Price ?? book.Price;

        _memoryCache?.Set(GetMemoryCacheKey(id), book);
        await _bookService.UpdateAsync(book);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<DetailedBookViewDto> AssignAuthorToBook(long id, long authorId)
    {
        var book = await _bookService.FindByIdAsync(id);
        var newAuthor = await _authorService.FindByIdAsync(authorId);

        var authors = book.Authors?.ToList() ?? new List<AuthorEntity>();

        if (authors.Any(auth => auth.Id == authorId))
        {
            throw new AuthorBookAssociationException(id, authorId);
        }

        authors.Add(newAuthor);
        book.Authors = authors;
        await _bookService.UpdateAsync(book);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<IEnumerable<GeneralBookViewDto>> FetchAllBooksAsync()
    {
        var books = await _bookService.FetchAllAsync();

        return _mapper.Map<List<GeneralBookViewDto>>(books);
    }

    public async Task<IEnumerable<GeneralBookViewDto>> FetchFilteredBooksAsync(BookFilterDto bookFilterDto)
    {
        var queryResult = await _bookService
            .FetchFilteredAsync(_mapper.Map<BookFilter>(bookFilterDto), 
                                _mapper.Map<QueryParams>(bookFilterDto));

        return _mapper.Map<List<GeneralBookViewDto>>(queryResult.Items);
    }

    public async Task<DetailedBookViewDto> FindBookByIdAsync(long id)
    {
        if (!(_memoryCache?.TryGetValue(GetMemoryCacheKey(id), out var cachedBook) ?? false))
        {
            cachedBook = await _bookService.FindByIdAsync(id);

            _memoryCache?.Set(GetMemoryCacheKey(id), cachedBook);
        }

        return _mapper.Map<DetailedBookViewDto>(cachedBook);
    }

    public async Task DeleteBookByIdAsync(long id)
    {
        var book = await _bookService.FindByIdAsync(id);
        _memoryCache?.Remove(book);
        await _bookService.DeleteAsync(book);
    }
}
