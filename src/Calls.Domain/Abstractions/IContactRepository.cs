using Calls.Domain.Entities;

namespace Calls.Domain.Abstractions;

public interface IContactRepository
{
    public Task<IEnumerable<Contact>> GetAllAsync();
    public Task<Contact> GetByIdAsync(Guid id);
    public Task CreateAsync(Contact contact);
    public Task UpdateAsync(Contact contact);
    public Task DeleteAsync(Guid id);
}