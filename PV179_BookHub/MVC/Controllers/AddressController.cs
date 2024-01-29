using BusinessLayer.Facades.Address;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.DTOs.Address.Create;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Models.Enums;

namespace MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class AddressController : Controller
{
    private readonly IAddressFacade _addressFacade;

    public AddressController(IAddressFacade addressFacade)
    {
        _addressFacade = addressFacade;
    }

    public async Task<IActionResult> Index()
    {
        var addresses = await _addressFacade.GetAllAddressesAsync();
        return View(addresses);
    }

    public async Task<IActionResult> Details(int id, bool updated)
    {
        var address = await _addressFacade.FindAddressByIdAsync(id);
        if (updated)
        {
            ViewBag.Message = "Address Saved Successfully";
        }
        return View(address);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAddressDto createAddressDto)
    {
        var created = await _addressFacade.CreateAddressAsync(createAddressDto);
        return RedirectToAction(nameof(Details), new { created.Id, updated = true });
    }


    public async Task<IActionResult> Edit(int id)
    {
        var addressDto = await _addressFacade.FindAddressByIdAsync(id);
        return View(addressDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CreateAddressDto updateAddressDto)
    {
        var updated = await _addressFacade.UpdateAddressAsync(id, updateAddressDto);
        return RedirectToAction(nameof(Details), new { id, updated = true });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var address = await _addressFacade.FindAddressByIdAsync(id);
        return View(address);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _addressFacade.DeleteAddressByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
