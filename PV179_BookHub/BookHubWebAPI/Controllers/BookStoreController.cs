using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;
using BusinessLayer.Facades.BookStore;
using DataAccessLayer.Models.Logistics;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookStoreController : ControllerBase
{
    private readonly IBookStoreFacade _bookStoreFacade;

    public BookStoreController(IBookStoreFacade bookStoreFacade)
    {
        _bookStoreFacade = bookStoreFacade;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookStores()
    {
        return Ok(await _bookStoreFacade.GetAllBookStores());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetBookStore(long id)
    {
        return Ok(await _bookStoreFacade.GetBookStore(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBookStore(CreateBookStoreDto createBookStoreDto)
    {
        var newBookStore = await _bookStoreFacade.CreateBookStore(createBookStoreDto);
        return Created(
            new Uri($"{Request.Path}/{newBookStore.BookStoreId}", UriKind.Relative), newBookStore);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateBookStore(long id, CreateBookStoreDto updateBookStoreDto)
    {
        return Ok(await _bookStoreFacade.UpdateBookStore(id, updateBookStoreDto));
    }
    

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteBookStore(long id)
    {
        await _bookStoreFacade.DeleteBookStore(id);
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

