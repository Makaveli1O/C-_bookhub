using AutoMapper;
using BookHubWebAPI.Api.Create;
using BookHubWebAPI.Api.View;
using DataAccessLayer.Models;
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
    [Route("{wishListId}")]
    public async Task<IActionResult> UpdateWishList(long wishListId, CreateWishListDto createWishListDto)
    {
        var wishList = await _unitOfWork.WishListRepository.GetByIdAsync(wishListId);
        
        if (wishList != null)
        {
            wishList.Description = createWishListDto.Description ?? wishList.Description;
            _unitOfWork.WishListRepository.Update(wishList);
            _unitOfWork.Commit();
        }

        return Ok(
            _mapper.Map<GeneralWishListViewDto>(wishList)
            );
    }

    [HttpPut]
    [Route("{wishListItemId}")]
    public async Task<IActionResult> UpdateWishListItem(long wishListItemId, CreateWishListItemDto createWishListDto)
    {
        var wishListItem = await _unitOfWork.WishListItemRepository.GetByIdAsync(wishListItemId);

        if (wishListItem != null)
        {
            wishListItem.WishListId = createWishListDto.WishListId;
            wishListItem.BookId = createWishListDto.BookId;
            wishListItem.PreferencePriorty = createWishListDto.PreferencePriorty;
        }

        return Ok(
            _mapper.Map<GeneralWishListViewDto>(wishListItem)
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
            _unitOfWork.Commit();
        }
        return NoContent();
    }

    [HttpDelete]
    [Route("{wishListItemId}/item")]
    public async Task<IActionResult> DeleteWishListItem(long wishListItemId)
    {
        var wishListItem = await _unitOfWork.WishListItemRepository.GetByIdAsync(wishListItemId);

        if (wishListItem != null)
        {
            _unitOfWork.WishListItemRepository.Delete(wishListItem);
            _unitOfWork.Commit();
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
            _unitOfWork.Commit();
        }
        return NoContent();
    }
}
