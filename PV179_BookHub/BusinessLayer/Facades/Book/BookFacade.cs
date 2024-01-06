﻿using AutoMapper;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.Filter;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using BusinessLayer.Services.Author;
using BusinessLayer.Services.Book;
using Infrastructure.Query;
using Infrastructure.Query.Filters;
using Infrastructure.Query.Filters.EntityFilters;

namespace BusinessLayer.Facades.Book;

public class BookFacade : BaseFacade, IBookFacade
{
    private readonly IGenericService<BookEntity, long> _bookService;
    private readonly IAuthorService _authorService;
    private readonly IGenericService<PublisherEntity, long> _publisherService;

    public BookFacade(IMapper mapper, IGenericService<BookEntity, long> bookService, IAuthorService authorService, 
        IGenericService<PublisherEntity, long> publisherService)
        : base(mapper)
    {
        _bookService = bookService;
        _authorService = authorService;
        _publisherService = publisherService;
    }

    public async Task<DetailedBookViewDto> CreateBookAsync(CreateBookDto createBookDto)
    {
        var publisher = await _publisherService.FindByIdAsync(createBookDto.PublisherId);
        var authors = await _authorService.FetchAllAuthorsByIdsAsync(createBookDto.AuthorIds);

        var book = _mapper.Map<BookEntity>(createBookDto);
        book.Authors = authors;
        book.Publisher = publisher;
        book = await _bookService.CreateAsync(book);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<DetailedBookViewDto> UpdateBookAsync(long id, CreateBookDto updateBookDto)
    {
        var book = await _bookService.FindByIdAsync(id);

        if (updateBookDto.PublisherId != book.PublisherId)
        {
            book.PublisherId = updateBookDto.PublisherId;
            book.Publisher = null;
        }

        book.Title = updateBookDto.Title ?? book.Title;
        book.ISBN = updateBookDto.ISBN ?? book.ISBN;
        book.BookGenre = updateBookDto.BookGenre;
        book.Description = updateBookDto.Description ?? book.Description;
        book.Price = updateBookDto.Price;

        var authors = await _authorService.FetchAllAuthorsByIdsAsync(updateBookDto.AuthorIds);
        book.Authors = authors;

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
        var book = await _bookService.FindByIdAsync(id);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task DeleteBookByIdAsync(long id)
    {
        var book = await _bookService.FindByIdAsync(id);

        await _bookService.DeleteAsync(book);
    }
}
