using AutoMapper;
using BookHubWebAPI.Api.Address.Create;
using BookHubWebAPI.Api.Address.View;
using DataAccessLayer.Models.Logistics;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AddressController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(CreateAddressDto createAddressDto)
    {
        var address = _mapper.Map<Address>(createAddressDto);

        await _unitOfWork.AddressRepository.AddAsync(address);
        await _unitOfWork.CommitAsync();

        return Created(
            new Uri($"{Request.Path}/{address.Id}", UriKind.Relative),
            _mapper.Map<DetailedAddressView>(address)
            );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAddress(long id, CreateAddressDto createAddressDto)
    {
        var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);

        if (address != null)
        {
            address.StreetNumber = createAddressDto.StreetNumber ?? address.StreetNumber;
            address.City = createAddressDto.City ?? address.City;
            address.State = createAddressDto.State ?? address.State;
            address.Street = createAddressDto.Street ?? address.Street;
            address.PostalCode = createAddressDto.PostalCode ?? address.PostalCode;

            _unitOfWork.AddressRepository.Update(address);
            await _unitOfWork.CommitAsync();
        }
        return Ok(
            _mapper.Map<DetailedAddressView>(address)
            );
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);

        return Ok(
            _mapper.Map<DetailedAddressView>(address)
            );
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAddress(long id)
    {
        var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);

        if (address != null)
        {
            _unitOfWork.AddressRepository.Delete(address);
            await _unitOfWork.CommitAsync();
        }

        return NoContent();
    }

}
