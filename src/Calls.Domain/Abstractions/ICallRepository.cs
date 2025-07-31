using Calls.Domain.Entities;

namespace Calls.Domain.Abstractions;

public interface ICallRepository
{
    public Task<IEnumerable<Call>> GetAllAsync();
    public Task<Call> GetByIdAsync(Guid id);
    public Task CreateAsync(Call call);
    public Task UpdateAsync(Call call);
    public Task DeleteAsync(Guid id);
}