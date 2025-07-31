using Calls.Application.Models;

namespace Calls.Application.Abstractions;

public interface ICallService
{
    public Task<IEnumerable<CallResponse>> GetCallsAsync();
    public Task<CallResponse> FindCallAsync(Guid id);
    public Task<CallResponse> CreateCallAsync(CreateCallRequest request);
    public Task UpdateCallAsync(UpdateCallRequest request);
    public Task FinishCallAsync(Guid id);
    public Task DeleteCallAsync(Guid id);
}