using Warehouse.Api.Application.BackgroundServices;

namespace Warehouse.Api.Application;

public static class Extensions
{
    public static void AddApplicationScopeResolvers(this IServiceCollection services)
    {
        services.AddHostedService<TemporaryFileCleaningBackgroundService>();
    }
}