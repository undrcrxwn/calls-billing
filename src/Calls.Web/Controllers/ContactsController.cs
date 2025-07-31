using Calls.Application.Abstractions;
using Calls.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calls.Web.Controllers;

public class ContactsController(IContactService contactService, IStatisticsService statisticsService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var contacts = await contactService.GetContactsAsync();
        return View(contacts);
    }

    [HttpGet]
    public async Task<IActionResult> Statistics(ContactStatisticsRequest request)
    {
        request.Since ??= DateTime.Now.AddDays(-7);
        request.Until ??= DateTime.Now;
        request.Statistics = await statisticsService.GetContactStatisticsAsync(request);
        return View(request);
    }

    [HttpGet]
    public IActionResult Create() => View(new CreateContactRequest());

    [HttpPost]
    public async Task<IActionResult> Create(CreateContactRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        await contactService.CreateContactAsync(request);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var contact = await contactService.FindContactAsync(id);
        return View(new UpdateContactRequest
        {
            Id = contact.Id,
            Name = contact.Name,
            Email = contact.Email,
            DateOfBirth = contact.DateOfBirth,
            ChargePerMinute = contact.ChargePerMinute,
            PhoneNumbers = contact.PhoneNumbers
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateContactRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        await contactService.UpdateContactAsync(request);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await contactService.DeleteContactAsync(id);
        return RedirectToAction("Index");
    }
}