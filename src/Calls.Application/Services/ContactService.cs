using Calls.Application.Abstractions;
using Calls.Application.Models;
using Calls.Domain.Abstractions;
using Calls.Domain.Entities;

namespace Calls.Application.Services;

public class ContactService(IContactRepository contactRepository, IPhoneNumberRepository phoneNumberRepository) : IContactService
{
    public async Task<IEnumerable<ContactResponse>> GetContactsAsync()
    {
        var contacts = await contactRepository.GetAllAsync();
        return contacts.Select(contact => ContactResponse.FromEntity(contact));
    }

    public async Task<ContactResponse> FindContactAsync(Guid id)
    {
        var contact = await contactRepository.GetByIdAsync(id);
        return ContactResponse.FromEntity(contact);
    }

    public async Task<ContactResponse> CreateContactAsync(CreateContactRequest request)
    {
        var contact = new Contact
        {
            Profile = new Profile
            {
                Name = request.Name,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth!.Value
            },
            ChargePerMinute = request.ChargePerMinute,
            PhoneNumbers = []
        };

        await contactRepository.CreateAsync(contact);
        return ContactResponse.FromEntity(contact);
    }

    public async Task UpdateContactAsync(UpdateContactRequest request)
    {
        var contact = await contactRepository.GetByIdAsync(request.Id);
        var foundPhoneNumbers = await phoneNumberRepository.GetManyAsync(request.PhoneNumbers);
        var phoneNumbers = foundPhoneNumbers
            .Zip(request.PhoneNumbers, (foundNumber, askedNumber) => foundNumber ?? new PhoneNumber(askedNumber)
            {
                ContactId = request.Id
            })
            .ToList();

        if (phoneNumbers.Any(number => number.ContactId is not null && number.ContactId != request.Id))
            throw new InvalidOperationException();

        contact.Profile.Name = request.Name;
        contact.Profile.Email = request.Email;
        contact.Profile.DateOfBirth = request.DateOfBirth;
        contact.ChargePerMinute = request.ChargePerMinute;
        contact.PhoneNumbers = phoneNumbers;

        await contactRepository.UpdateAsync(contact);
    }

    public async Task DeleteContactAsync(Guid id)
    {
        await contactRepository.DeleteAsync(id);
    }
}