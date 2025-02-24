using Contracts.Common.Interfaces;
using Infrastructure.Common;
using NotificationService.Repositories;
using NotificationService.Repositories.Interfaces;
using StackExchange.Redis;

namespace NotificationService.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services) =>
        services.AddScoped<INotificationRepository, NotificationRepository>()
            .AddTransient<ISerializeService, SerializeService>();

    public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfiguration = configuration.GetSection("CacheSettings:ConnectionString").Value;
        if (string.IsNullOrWhiteSpace(redisConfiguration))
            throw new ArgumentNullException("Redis configuration is missing");
        
        // Redis Configuration
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfiguration;
        });
    }
}