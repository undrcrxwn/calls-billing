using Calls.Application.Models;

namespace Calls.Application.Abstractions;

public interface IContactService
{
    public Task<IEnumerable<ContactResponse>> GetContactsAsync();
    public Task<ContactResponse> FindContactAsync(Guid id);
    public Task<ContactResponse> CreateContactAsync(CreateContactRequest request);
    public Task UpdateContactAsync(UpdateContactRequest request);
    public Task DeleteContactAsync(Guid id);
}