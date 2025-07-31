using Calls.Application.Abstractions;
using Calls.Application.Models;
using Calls.Domain.Abstractions;
using Calls.Domain.Entities;
using InvalidOperationException = System.InvalidOperationException;

namespace Calls.Application.Services;

public class CallService(ICallRepository callRepository, IPhoneNumberRepository phoneNumberRepository) : ICallService
{
    public async Task<IEnumerable<CallResponse>> GetCallsAsync()
    {
        var calls = await callRepository.GetAllAsync();
        return calls.Select(call => CallResponse.FromEntity(call));
    }

    public async Task<CallResponse> FindCallAsync(Guid id)
    {
        var call = await callRepository.GetByIdAsync(id);
        return CallResponse.FromEntity(call);
    }

    public async Task<CallResponse> CreateCallAsync(CreateCallRequest request)
    {
        var starterPhoneNumber = await phoneNumberRepository.GetAsync(request.StarterPhoneNumber);
        var participantPhoneNumber = await phoneNumberRepository.GetAsync(request.ParticipantPhoneNumber);
        
        if (starterPhoneNumber.Contact is null)
            throw new InvalidOperationException();
        
        var contact = new Call
        {
            Since = DateTime.Now,
            StarterPhoneNumber = starterPhoneNumber,
            ChargePerMinute = starterPhoneNumber.Contact.ChargePerMinute,
            ParticipantPhoneNumbers = new List<PhoneNumber>
            {
                participantPhoneNumber
            }
        };

        await callRepository.CreateAsync(contact);
        return CallResponse.FromEntity(contact);
    }

    public async Task UpdateCallAsync(UpdateCallRequest request)
    {
        var call = await callRepository.GetByIdAsync(request.Id);
        var starterPhoneNumber = await phoneNumberRepository.GetAsync(request.StarterPhoneNumber);
        var participantPhoneNumbers = await phoneNumberRepository.GetManyAsync(request.ParticipantPhoneNumbers);

        if (participantPhoneNumbers.Contains(null))
            throw new InvalidOperationException();

        call.Since = request.Since;
        call.Until = request.Until;
        call.ChargePerMinute = request.ChargePerMinute;
        call.Charge = call.ChargeForNow;
        call.StarterPhoneNumber = starterPhoneNumber;
        call.ParticipantPhoneNumbers = participantPhoneNumbers!;

        await callRepository.UpdateAsync(call);
    }

    public async Task FinishCallAsync(Guid id)
    {
        var call = await callRepository.GetByIdAsync(id);

        if (call.Until is not null)
            throw new InvalidOperationException("Call has already been finished.");

        call.Until = DateTime.Now;
        call.DurationInMinutes = call.DurationForNow.TotalMinutes;
        call.Charge = call.ChargeForNow;
        
        await callRepository.UpdateAsync(call);
    }

    public async Task DeleteCallAsync(Guid id)
    {
        await callRepository.DeleteAsync(id);
    }
}