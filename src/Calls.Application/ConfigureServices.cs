using Calls.Application.Abstractions;
using Calls.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Calls.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) => services
        .AddScoped<ICallService, CallService>()
        .AddScoped<IContactService, ContactService>()
        .AddScoped<IStatisticsService, StatisticsService>();
}