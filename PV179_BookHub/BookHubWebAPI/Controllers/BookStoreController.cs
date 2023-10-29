using AutoMapper;
using BookHubWebAPI.Api.BookStore.Create;
using BookHubWebAPI.Api.BookStore.View;
using DataAccessLayer.Models;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookStoreController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookStoreController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookStores()
    {
        var bookStore = await _unitOfWork.BookStoreRepository.GetAllAsync();
        return Ok(_mapper.Map<List<DetailedBookStoreViewDto>>(bookStore));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetBookStore(long id)
    {
        var bookStore = await _unitOfWork.BookStoreRepository.GetByIdAsync(id);
        return Ok(_mapper.Map<DetailedBookStoreViewDto>(bookStore));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookStore(CreateBookStoreDto createBookStoreDto)
    {
        var bookStore = _mapper.Map<BookStore>(createBookStoreDto);

        await _unitOfWork.BookStoreRepository.AddAsync(bookStore);
        await _unitOfWork.CommitAsync();

        return Created(
            new Uri($"{Request.Path}/{bookStore.Id}", UriKind.Relative),
            _mapper.Map<DetailedBookStoreViewDto>(bookStore));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateBookStore(long id, CreateBookStoreDto createBookStoreDto)
    {
        var bookStore = await _unitOfWork.BookStoreRepository.GetByIdAsync(id);
        if (bookStore != null)
        {
            bookStore.AddressId = createBookStoreDto.AddressId;
            bookStore.ManagerId = createBookStoreDto.ManagerId;
            bookStore.Name = createBookStoreDto.Name;
            bookStore.PhoneNumber = createBookStoreDto.PhoneNumber;
            bookStore.Email = createBookStoreDto.Email;

            _unitOfWork.BookStoreRepository.Update(bookStore);
            await _unitOfWork.CommitAsync();
        }
        return Ok(_mapper.Map<DetailedBookStoreViewDto>(bookStore));
    }


    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteBookStore(long id)
    {
        var bookStore = await _unitOfWork.BookStoreRepository.GetByIdAsync(id);
        if (bookStore != null)
        {
            _unitOfWork.BookStoreRepository.Delete(bookStore);
            await _unitOfWork.CommitAsync();
        }
        return NoContent();
    }


    [HttpGet]
    [Route("Inventory")]
    public async Task<IActionResult> GetAllInventoryItems()
    {
        var inventoryItems = await _unitOfWork.InventoryItemRepository.GetAllAsync();
        return Ok(_mapper.Map<List<DetailedInventoryItemViewDto>>(inventoryItems));
    }

    [HttpGet]
    [Route("Inventory/{id}")]
    public async Task<IActionResult> GetInventoryItem(long id)
    {
        var inventoryItems = await _unitOfWork.InventoryItemRepository.GetByIdAsync(id);
        return Ok(_mapper.Map<DetailedInventoryItemViewDto>(inventoryItems));
    }

    [HttpPost]
    [Route("Inventory")]
    public async Task<IActionResult> CreateInventoryItem(CreateInventoryItemDto createInventoryItemDto)
    {
        var inventoryItem = _mapper.Map<InventoryItem>(createInventoryItemDto);

        await _unitOfWork.InventoryItemRepository.AddAsync(inventoryItem);
        await _unitOfWork.CommitAsync();

        return Created(
            new Uri($"{Request.Path}/{inventoryItem.Id}", UriKind.Relative),
            _mapper.Map<DetailedInventoryItemViewDto>(inventoryItem));
    }

    [HttpPut]
    [Route("Inventory/{id}")]
    public async Task<IActionResult> UpdateInventoryItem(long id, CreateInventoryItemDto createInventoryItemDto)
    {
        var inventoryItem = await _unitOfWork.InventoryItemRepository.GetByIdAsync(id);
        if (inventoryItem != null)
        {
            inventoryItem.InStock = createInventoryItemDto.InStock;
            inventoryItem.LastRestock = createInventoryItemDto.LastRestock;
            inventoryItem.BookStoreId = createInventoryItemDto.BookStoreId;
            inventoryItem.BookId = createInventoryItemDto.BookId;

            _unitOfWork.InventoryItemRepository.Update(inventoryItem);
            await _unitOfWork.CommitAsync();
        }
        return Ok(_mapper.Map<DetailedInventoryItemViewDto>(inventoryItem));
    }

    [HttpDelete]
    [Route("Inventory/{id}")]
    public async Task<IActionResult> DeleteInventoryItem(long id)
    {
        var inventoryItem = await _unitOfWork.InventoryItemRepository.GetByIdAsync(id);
        if (inventoryItem != null)
        {
            _unitOfWork.InventoryItemRepository.Delete(inventoryItem);
            await _unitOfWork.CommitAsync();
        }
        return NoContent();
    }
}

