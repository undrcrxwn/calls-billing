using Calls.Domain.Abstractions;
using Calls.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calls.Persistence.Services;

public class CallRepository(AppDbContext context) : ICallRepository
{
    public async Task<IEnumerable<Call>> GetAllAsync()
    {
        var calls = await context.Calls
            .Include(call => call.StarterPhoneNumber)
            .Include(call => call.ParticipantPhoneNumbers)
            .OrderByDescending(call => call.Since)
            .ToListAsync();

        return calls;
    }

    public async Task<Call> GetByIdAsync(Guid id)
    {
        return await context.Calls
            .Include(call => call.StarterPhoneNumber)
            .Include(call => call.ParticipantPhoneNumbers)
            .FirstAsync(call => call.Id == id);
    }

    public async Task CreateAsync(Call call)
    {
        context.Calls.Add(call);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Call call)
    {
        context.Calls.Update(call);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await context.Calls.Where(call => call.Id == id).ExecuteDeleteAsync();
    }
}