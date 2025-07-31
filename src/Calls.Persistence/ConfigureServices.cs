using Calls.Application.Abstractions;
using Calls.Domain.Abstractions;
using Calls.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Calls.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services) => services
        .AddScoped<IContactRepository, ContactRepository>()
        .AddScoped<ICallRepository, CallRepository>()
        .AddScoped<IPhoneNumberRepository, PhoneNumberRepository>()
        .AddScoped<IStatisticsRepository, StatisticsRepository>()
        .AddDbContext<AppDbContext>();
}