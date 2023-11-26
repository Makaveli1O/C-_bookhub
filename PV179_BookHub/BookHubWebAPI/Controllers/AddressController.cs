using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.Facades.Address;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : Controller
{
    private readonly IAddressFacade _addressFacade;
    public AddressController(IAddressFacade addressFacade)
    {
        _addressFacade = addressFacade;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(CreateAddressDto createAddressDto)
    {
        var address = await _addressFacade.CreateAddressAsync(createAddressDto);

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
        var address = await _addressFacade.UpdateAddressAsync(id, createAddressDto);

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
        var address = await _addressFacade.FindAddressByIdAsync(id);

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
        await _addressFacade.DeleteAddressByIdAsync(id);
        return NoContent();
    }

}
