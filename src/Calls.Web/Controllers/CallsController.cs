using Calls.Application.Abstractions;
using Calls.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calls.Web.Controllers;

public class CallsController(ICallService callService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var calls = await callService.GetCallsAsync();
        return View(calls);
    }

    [HttpGet]
    public IActionResult Create() => View(new CreateCallRequest());

    [HttpPost]
    public async Task<IActionResult> Create(CreateCallRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);
        
        await callService.CreateCallAsync(request);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var call = await callService.FindCallAsync(id);
        return View(new UpdateCallRequest
        {
            Id = call.Id,
            StarterPhoneNumber = call.StarterPhoneNumber,
            ParticipantPhoneNumbers = call.ParticipantPhoneNumbers,
            Since = call.Since,
            Until = call.Until,
            ChargePerMinute = call.ChargePerMinute
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateCallRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);
        
        await callService.UpdateCallAsync(request);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Finish(Guid id)
    {
        await callService.FinishCallAsync(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await callService.DeleteCallAsync(id);
        return RedirectToAction("Index");
    }
}