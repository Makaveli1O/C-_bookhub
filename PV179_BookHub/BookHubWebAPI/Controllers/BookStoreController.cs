using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.Facades.BookStore;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookStoreController : ControllerBase
{
    private readonly IBookStoreFacade _bookStoreFacade;
    private readonly IInventoryItemFacade _inventoryItemFacade;

    public BookStoreController(IBookStoreFacade bookStoreFacade, IInventoryItemFacade inventoryItemFacade)
    {
        _bookStoreFacade = bookStoreFacade;
        _inventoryItemFacade = inventoryItemFacade;
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
            new Uri($"{Request.Path}/{newBookStore.Id}", UriKind.Relative), newBookStore);
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
        return Ok(await _inventoryItemFacade.GetAllInventoryItems());
    }

    [HttpGet]
    [Route("Inventory/{id}")]
    public async Task<IActionResult> GetInventoryItem(long id)
    {
        return Ok(await _inventoryItemFacade.GetInventoryItem(id));
    }

    [HttpPost]
    [Route("Inventory")]
    public async Task<IActionResult> CreateInventoryItem(CreateInventoryItemDto createInventoryItemDto)
    {
        var newInventoryItem = await _inventoryItemFacade.CreateInventoryItem(createInventoryItemDto);
        return Created(
            new Uri($"{Request.Path}/{newInventoryItem.BookStoreId}", UriKind.Relative), newInventoryItem);
    }

    [HttpPut]
    [Route("Inventory/{id}")]
    public async Task<IActionResult> UpdateInventoryItem(long id, CreateInventoryItemDto updateInventoryItemDto)
    {
        return Ok(await _inventoryItemFacade.UpdateInventoryItem(id, updateInventoryItemDto));
    }

    [HttpDelete]
    [Route("Inventory/{id}")]
    public async Task<IActionResult> DeleteInventoryItem(long id)
    {
        await _inventoryItemFacade.DeleteInventoryItem(id);
        return NoContent();
    }
}

