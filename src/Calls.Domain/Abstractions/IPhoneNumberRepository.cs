using Calls.Domain.Entities;

namespace Calls.Domain.Abstractions;

public interface IPhoneNumberRepository
{
    public Task<PhoneNumber> GetAsync(string phoneNumber);
    public Task<IList<PhoneNumber?>> GetManyAsync(IEnumerable<string> phoneNumbers);
}