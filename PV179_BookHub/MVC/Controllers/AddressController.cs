using BusinessLayer.Facades.Address;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.DTOs.Address.Create;

namespace MVC.Controllers;

public class AddressController : Controller
{
    private readonly IAddressFacade _addressFacade;
    private readonly UserManager<LocalIdentityUser> _userManager;

    public AddressController(IAddressFacade addressFacade, UserManager<LocalIdentityUser> userManager)
    {
        _addressFacade = addressFacade;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        Console.WriteLine("HEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
        var addresses = await _addressFacade.GetAllAddressesAsync();
        Console.WriteLine("HEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
        return View(addresses);
    }

    public async Task<IActionResult> Details(int id)
    {
        var address = await _addressFacade.FindAddressByIdAsync(id);
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
        await _addressFacade.CreateAddressAsync(createAddressDto);
        ViewBag.Message = "Address Created Successfully";
        return View(createAddressDto);
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
        ViewBag.Message = "Address Updated Successfully";
        return View(updated);
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
