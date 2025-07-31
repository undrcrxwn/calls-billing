using Calls.Application.Models;
using Calls.Domain.Abstractions;
using Calls.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calls.Persistence.Services;

public class ContactRepository(AppDbContext context) : IContactRepository
{
    public async Task<IEnumerable<Contact>> GetAllAsync() => await context.Contacts
        .Include(contact => contact.Profile)
        .Include(contact => contact.PhoneNumbers)
        .ToListAsync();

    public async Task<Contact> GetByIdAsync(Guid id)
    {
        return await context.Contacts
            .Include(contact => contact.Profile)
            .Include(contact => contact.PhoneNumbers)
            .FirstAsync(contact => contact.Id == id);
    }

    public async Task CreateAsync(Contact contact)
    {
        context.Contacts.Add(contact);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        var existingContact = await GetByIdAsync(contact.Id);
        var existingPhoneNumbers = existingContact.PhoneNumbers.ToList();
        var updatedPhoneNumbers = contact.PhoneNumbers.ToList();

        context.Entry(existingContact).CurrentValues.SetValues(contact);
        
        var phoneNumbersToRemove = existingPhoneNumbers
            .Where(number => updatedPhoneNumbers.All(updatedNumber => updatedNumber.Value != number.Value))
            .ToList();
        
        foreach (var phoneNumber in phoneNumbersToRemove)
            existingContact.PhoneNumbers.Remove(phoneNumber);

        var phoneNumbersToAdd = updatedPhoneNumbers
            .Where(updatedNumber => existingPhoneNumbers.All(number => number.Value != updatedNumber.Value))
            .ToList();
        
        foreach (var phoneNumber in phoneNumbersToAdd)
        {
            var isNumberTaken = await context.PhoneNumbers
                .Where(number => number.Value == phoneNumber.Value)
                .AnyAsync(number => number.ContactId != contact.Id);
            
            if (isNumberTaken)
                throw new InvalidOperationException($"Phone number {phoneNumber.Value} already belongs to another contact.");
            
            existingContact.PhoneNumbers.Add(phoneNumber);
        }

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await context.Contacts.Where(call => call.Id == id).ExecuteDeleteAsync();
    }
}