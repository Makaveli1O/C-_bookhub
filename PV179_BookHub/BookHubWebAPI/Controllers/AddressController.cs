using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.Services;
using AutoMapper;
using DataAccessLayer.Models.Logistics;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : Controller
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(CreateAddressDto createAddressDto)
    {
        var address = await _addressService.CreateAddressAsync(createAddressDto);

        if (address == null)
        {
            return BadRequest();
        }

        return Created(
            new Uri($"{Request.Path}/{address.Id}", UriKind.Relative),
            address
            );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAddress(long id, CreateAddressDto createAddressDto)
    {
        var address = await _addressService.UpdateAddressAsync(id, createAddressDto);

        if (address == null)
        {
            return BadRequest();
        }

        return Ok(address);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        var address = await _addressService.FindAddressByIdAsync(id);

        if (address == null)
        {
            return NotFound();
        }

        return Ok(address);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAddress(long id)
    {
        bool wasDeleted = await _addressService.DeleteAddressByIdAsync(id);

        if (wasDeleted)
        {
            return Ok();
        }

        return BadRequest();
    }

}
