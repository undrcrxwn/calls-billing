using Calls.Application.Models;

namespace Calls.Application.Abstractions;

public interface IStatisticsService
{
    public Task<StatisticsResponse> GetContactStatisticsAsync(ContactStatisticsRequest request);
    public Task<StatisticsResponse> GetPhoneNumberStatisticsAsync(PhoneNumberStatisticsRequest request);
}