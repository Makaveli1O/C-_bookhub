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

    private static void CheckForMultiplePrimaryAuthors(string title, IEnumerable<AuthorBookAssociationDto>? authorAssocDto)
    {
        if (authorAssocDto?.Count(author => author.IsPrimary) > 1)
        {
            throw new MultiplePrimaryAuthorsException(title);
        }
    }

    private static IEnumerable<Tuple<long, bool>> ConvertAuthorAssociationDto(IEnumerable<AuthorBookAssociationDto>? authorAssocDto)
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
            book.Publisher = await _publisherService.FindByIdAsync(book.PublisherId);
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

    private static void CheckAuthorBookAssociations(long bookId, 
        List<AuthorBookAssociationEntity> authorBookAssociations, bool isPrimary, bool force)
    {
        bool primaryExist = authorBookAssociations.Any(assoc => assoc.IsPrimary);

        if (isPrimary && primaryExist)
        {
            if (!force)
            {
                throw new MultiplePrimaryAuthorsException(bookId);
            }
            else
            {
                authorBookAssociations.ForEach(assoc => assoc.IsPrimary = false);
            }
        }
    }

    private static IEnumerable<AuthorBookAssociationEntity> CheckAndModifyNewAuthorBookAssociations(
        long bookId, List<AuthorBookAssociationEntity> authorBookAssociations, 
        AuthorEntity newAuthor, bool isPrimary, bool force)
    {
        CheckAuthorBookAssociations(bookId, authorBookAssociations, isPrimary, force);

        authorBookAssociations
            .Add(new AuthorBookAssociationEntity() 
            { 
                Author =  newAuthor, 
                BookId = bookId,
                IsPrimary = isPrimary 
            });

        return authorBookAssociations;
    }

    public async Task<DetailedBookViewDto> AssignAuthorToBookAsync(long id, AuthorBookAssociationDto authorBookAssociation, bool force = false)
    {
        var book = await _bookService.FindByIdAsync(id);
        var newAuthor = await _authorService.FindByIdAsync(authorBookAssociation.Id);

        var authors = book.Authors?.ToList() ?? new List<AuthorEntity>();

        if (authors.Any(auth => auth.Id == authorBookAssociation.Id))
        {
            throw new AuthorBookAssociationException(id, authorBookAssociation.Id);
        }
        book.AuthorBookAssociations = CheckAndModifyNewAuthorBookAssociations(
            book.Id, book.AuthorBookAssociations?.ToList() ?? new(), newAuthor, 
            authorBookAssociation.IsPrimary, force);

        await _bookService.UpdateAsync(book);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task UnassignAuthorFromBookAsync(long bookId, long authorId)
    {
        var assoc = await _authorBookAsssociationService.FindByBookAndAuthorIdAsync(bookId, authorId);
        await _authorBookAsssociationService.DeleteAsync(assoc);
    }

    private static IEnumerable<AuthorBookAssociationEntity> CheckAndModifyExistingAuthorBookAssociations(
        long bookId, long authorId, List<AuthorBookAssociationEntity> authorBookAssociations, 
        bool isPrimary, bool force)
    {
        CheckAuthorBookAssociations(bookId, authorBookAssociations, isPrimary, force);

        var assoc = authorBookAssociations.FirstOrDefault(x => x.BookId == bookId && x.AuthorId == authorId);
        if (assoc == null)
        {
            throw new NoSuchEntityException<long>(typeof(AuthorBookAssociationEntity));
        }
        assoc.IsPrimary = isPrimary;

        return authorBookAssociations;
    }

    public async Task<DetailedBookViewDto> MakeUnmakeAuthorPrimaryAsync(long bookId, AuthorBookAssociationDto authorBookAssociation, bool force = false)
    {
        var book = await _bookService.FindByIdAsync(bookId);
        var assocs = book.AuthorBookAssociations?.ToList() ?? new();
        if (assocs.Count == 0)
        {
            throw new NoSuchEntityException<long>(typeof(AuthorBookAssociationEntity));
        }

        book.AuthorBookAssociations = CheckAndModifyExistingAuthorBookAssociations(bookId, 
            authorBookAssociation.Id, assocs, authorBookAssociation.IsPrimary, force);

        await _bookService.UpdateAsync(book);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<IEnumerable<GeneralBookViewDto>> FetchAllBooksAsync()
    {
        var books = await _bookService.FetchAllAsync();

        return _mapper.Map<List<GeneralBookViewDto>>(books);
    }

    public async Task<BookFilterResultDto> FetchFilteredBooksAsync(BookFilterDto bookFilterDto)
    {
        var queryResult = await _bookService
            .FetchFilteredAsync(_mapper.Map<BookFilter>(bookFilterDto), 
                                _mapper.Map<QueryParams>(bookFilterDto));

        return _mapper.Map<BookFilterResultDto>(queryResult);
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
