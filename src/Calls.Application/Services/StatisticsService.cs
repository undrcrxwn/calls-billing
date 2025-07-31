using Calls.Application.Abstractions;
using Calls.Application.Models;

namespace Calls.Application.Services;

public class StatisticsService(IStatisticsRepository repository) : IStatisticsService
{
    public async Task<StatisticsResponse> GetContactStatisticsAsync(ContactStatisticsRequest request) =>
        await repository.GetContactStatisticsAsync(request.Id, request.Since!.Value, request.Until!.Value);
    
    public async Task<StatisticsResponse> GetPhoneNumberStatisticsAsync(PhoneNumberStatisticsRequest request) =>
        await repository.GetPhoneNumberStatisticsAsync(request.PhoneNumber, request.Since, request.Until);
}