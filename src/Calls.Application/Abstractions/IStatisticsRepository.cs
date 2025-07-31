using Calls.Application.Models;

namespace Calls.Application.Abstractions;

public interface IStatisticsRepository
{
    public Task<StatisticsResponse> GetContactStatisticsAsync(Guid id, DateTime since, DateTime until);
    public Task<StatisticsResponse> GetPhoneNumberStatisticsAsync(string phoneNumber, DateTime since, DateTime until);
}