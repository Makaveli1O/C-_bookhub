using AutoMapper;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Preferences;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WishListController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishListController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpPost("createWishList")]
    public async Task<IActionResult> CreateWishList(CreateWishListDto createWishListDto)
    {
        var wishList = _mapper.Map<WishList>(createWishListDto);
        
        await _unitOfWork.WishListRepository.AddAsync(wishList);
        await _unitOfWork.CommitAsync();

        return Created(
             new Uri($"{Request.Path}/{wishList.Id}", UriKind.Relative),
            _mapper.Map<GeneralWishListViewDto>(wishList));
    }

    [HttpPost("addedItemToWishList")]
    public async Task<IActionResult> CreateWishListItem(CreateWishListItemDto createWishListItemDto)
    {
        var wishListItem = _mapper.Map<WishListItem>(createWishListItemDto);

        await _unitOfWork.WishListItemRepository.AddAsync(wishListItem);
        await _unitOfWork.CommitAsync();

        return Created(
             new Uri($"{Request.Path}/{wishListItem.Id}", UriKind.Relative),
            _mapper.Map<GeneralWishListItemViewDto>(wishListItem));
    }

    [HttpPut]
    [Route("updateWishList/{wishListId}")]
    public async Task<IActionResult> UpdateWishList(long wishListId, string? wishListDescription)
    {
        var wishList = await _unitOfWork.WishListRepository.GetByIdAsync(wishListId);
        
        if (wishList != null)
        {
            wishList.Description = wishListDescription;
            _unitOfWork.WishListRepository.Update(wishList);
            await _unitOfWork.CommitAsync();
        }

        return Ok(
            _mapper.Map<GeneralWishListViewDto>(wishList)
            );
    }

    [HttpPut]
    [Route("updateWishListItem/{wishListItemId}")]
    public async Task<IActionResult> UpdateWishListItem(long wishListItemId, uint preferencePriorituy)
    {
        var wishListItem = await _unitOfWork.WishListItemRepository.GetByIdAsync(wishListItemId);

        if (wishListItem != null)
        {
            wishListItem.PreferencePriorty = preferencePriorituy;
            _unitOfWork.WishListItemRepository.Update(wishListItem);
            await _unitOfWork.CommitAsync();
        }

        return Ok(
            _mapper.Map<GeneralWishListItemViewDto>(wishListItem)
            );
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchWishList(long id)
    {
        var wishList = await _unitOfWork.WishListRepository.GetByIdAsync(id);

        return Ok(
            _mapper.Map<GeneralWishListViewDto>(wishList)
            );
    }

    [HttpGet]
    [Route("all/{wishListId}")]
    public async Task<IActionResult> FetchAllItemsFromWishList(long wishListId)
    {
        var wishList = await _unitOfWork.WishListRepository.GetByIdAsync(wishListId);

        return Ok(
            _mapper.Map<IEnumerable<GeneralWishListItemViewDto>>(wishList?.WishListItems)
            );
    }

    [HttpGet]
    [Route("wishList/{itemId}")]
    public async Task<IActionResult> FetchSingleItemFromWishList(long itemId)
    {
        var wishListItem = await _unitOfWork.WishListItemRepository.GetByIdAsync(itemId);

        return Ok(
               _mapper.Map<GeneralWishListItemViewDto>(wishListItem)
               );
    }

    [HttpDelete]
    [Route("{wishListId}")]
    public async Task<IActionResult> DeleteWishList(long wishListId)
    {
        var wishList = await _unitOfWork.WishListRepository.GetByIdAsync(wishListId);

        if (wishList != null)
        {
            _unitOfWork.WishListRepository.Delete(wishList);
            await _unitOfWork.CommitAsync();
        }
        return NoContent();
    }

    [HttpDelete]
    [Route("item/{wishListItemId}")]
    public async Task<IActionResult> DeleteWishListItem(long wishListItemId)
    {
        var wishListItem = await _unitOfWork.WishListItemRepository.GetByIdAsync(wishListItemId);

        if (wishListItem != null)
        {
            _unitOfWork.WishListItemRepository.Delete(wishListItem);
            await _unitOfWork.CommitAsync();
        }
        return NoContent();
    }

    [HttpDelete]
    [Route("{wishListId}/items")]
    public async Task<IActionResult> DeleteWishListItems(long wishListId)
    {
        var wishList = await _unitOfWork.WishListRepository.GetByIdAsync(wishListId);

        if (wishList != null)
        {
            wishList.WishListItems?.ToList().ForEach(_unitOfWork.WishListItemRepository.Delete);
            await _unitOfWork.CommitAsync();
        }
        return NoContent();
    }
}
