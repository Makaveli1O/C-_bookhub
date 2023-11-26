using AutoMapper;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using BusinessLayer.Facades.WishList;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Preferences;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WishListController : ControllerBase
{
    private readonly IWishListFacade _wishListFacade;

    public WishListController(IWishListFacade wishListFacade)
    {
        _wishListFacade = wishListFacade;
    }
    
    [HttpPost("createWishList")]
    public async Task<IActionResult> CreateWishList(CreateWishListDto createWishListDto)
    {
        var wishList = await _wishListFacade.CreateWishListAsync(createWishListDto);
        
        return Created(
             new Uri($"{Request.Path}/{wishList.Id}", UriKind.Relative),
             wishList);
    }

    [HttpPost("addedItemToWishList")]
    public async Task<IActionResult> CreateWishListItem(CreateWishListItemDto createWishListItemDto)
    {
        var wishListItem = await _wishListFacade.CreateWishListItemAsync(createWishListItemDto);
     
        return Created(
             new Uri($"{Request.Path}/{wishListItem.Id}", UriKind.Relative),
            wishListItem);
    }

    [HttpPut]
    [Route("updateWishList/{wishListId}")]
    public async Task<IActionResult> UpdateWishList(long wishListId, string? wishListDescription)
    {
        return Ok(await _wishListFacade.UpdateWishListAsync(wishListId, wishListDescription));
    }

    [HttpPut]
    [Route("updateWishListItem/{wishListItemId}")]
    public async Task<IActionResult> UpdateWishListItem(long wishListItemId, uint preferencePriority)
    {
        return Ok(await _wishListFacade.UpdateWishListItemAsync(wishListItemId, preferencePriority));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchWishList(long id)
    {
        return Ok(await _wishListFacade.FetchWishListAsync(id));
    }

    [HttpGet]
    [Route("all/{wishListId}")]
    public async Task<IActionResult> FetchAllItemsFromWishList(long wishListId)
    {
        return Ok(await _wishListFacade.FetchAllItemsFromWishListAsync(wishListId));
    }

    [HttpGet]
    [Route("wishList/{itemId}")]
    public async Task<IActionResult> FetchSingleItemFromWishList(long itemId)
    {
        return Ok(await _wishListFacade.FetchSingleItemFromWishListAsync(itemId));
    }

    [HttpDelete]
    [Route("{wishListId}")]
    public async Task<IActionResult> DeleteWishList(long wishListId)
    {
        await _wishListFacade.DeleteWishListAsync(wishListId);
        return NoContent();
    }

    [HttpDelete]
    [Route("item/{wishListItemId}")]
    public async Task<IActionResult> DeleteWishListItem(long wishListItemId)
    {
        await _wishListFacade.DeleteWishListItemAsync(wishListItemId);
        return NoContent();
    }

    [HttpDelete]
    [Route("{wishListId}/items")]
    public async Task<IActionResult> DeleteWishListItems(long wishListId)
    {
        await _wishListFacade.DeleteWishListItemsAsync(wishListId);
        return NoContent();
    }
}
