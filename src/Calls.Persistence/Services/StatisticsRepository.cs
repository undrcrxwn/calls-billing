using Calls.Application.Abstractions;
using Calls.Application.Models;
using Calls.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calls.Persistence.Services;

public class StatisticsRepository(AppDbContext context) : IStatisticsRepository
{
    public async Task<StatisticsResponse> GetContactStatisticsAsync(Guid id, DateTime since, DateTime until)
    {
        var startedCalls = context.Calls
            .Where(call => call.StarterPhoneNumber.ContactId == id);
        
        var joinedCalls = context.Calls
            .Where(call => call.ParticipantPhoneNumbers.Any(number => number.ContactId == id));

        return await GetStatisticsAsync(startedCalls, joinedCalls, since, until);
    }
    
    public async Task<StatisticsResponse> GetPhoneNumberStatisticsAsync(string phoneNumber, DateTime since, DateTime until)
    {
        var startedCalls = context.Calls
            .Where(call => call.StarterPhoneNumber.Value == phoneNumber);
        
        var joinedCalls = context.Calls
            .Where(call => call.ParticipantPhoneNumbers.Any(number => number.Value == phoneNumber));

        return await GetStatisticsAsync(startedCalls, joinedCalls, since, until);
    }
    
    public async Task<StatisticsResponse> GetStatisticsAsync(IQueryable<Call> startedCalls, IQueryable<Call> joinedCalls, DateTime since, DateTime until)
    {
        var startedCallsStatistics = await startedCalls
            .Where(call => call.Since >= since && call.Since <= until)
            .GroupBy(_ => 1)
            .Select(calls => new
            {
                StartedCallsCount = calls.Count(),
                StartedCallsDuration = calls.Sum(call => call.DurationInMinutes),
                StartedCallsCharge = calls.Sum(call => call.Charge),
                AverageStartedCallDuration = calls.Average(call => call.DurationInMinutes),
                AverageStartedCallCharge = calls.Average(call => call.Charge)
            })
            .FirstOrDefaultAsync();

        return new StatisticsResponse
        {
            CallsStarted = startedCallsStatistics?.StartedCallsCount ?? 0,
            CallsJoined = await joinedCalls.CountAsync(call => call.Since >= since && call.Since <= until),
            StartedCallDuration = startedCallsStatistics?.StartedCallsDuration ?? 0,
            AverageCallDuration = startedCallsStatistics?.AverageStartedCallDuration ?? 0,
            AverageCharge = startedCallsStatistics?.AverageStartedCallCharge ?? 0,
            TotalCharge = startedCallsStatistics?.StartedCallsCharge ?? 0
        };
    }
}