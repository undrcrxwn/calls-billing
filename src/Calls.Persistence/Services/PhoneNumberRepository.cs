using Calls.Domain.Abstractions;
using Calls.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calls.Persistence.Services;

public class PhoneNumberRepository(AppDbContext context) : IPhoneNumberRepository
{
    public async Task<PhoneNumber> GetAsync(string phoneNumber)
    {
        return await context.PhoneNumbers
            .Include(phone => phone.Contact)
            .FirstAsync(phone => phone.Value == phoneNumber);
    }

    public async Task<IList<PhoneNumber?>> GetManyAsync(IEnumerable<string> phoneNumbers)
    {
        var foundPhoneNumbers = await context.PhoneNumbers
            .Include(phone => phone.Contact)
            .Where(number => phoneNumbers.Contains(number.Value))
            .ToListAsync();
        
        return phoneNumbers
            .Select(askedNumber => foundPhoneNumbers.Find(foundNumber => foundNumber.Value == askedNumber))
            .ToList();
    }
}