using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using NotificationService.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace NotificationService.Repositories;

public class NotificationRepository : INotificationRepository
{
    private const string NOTIFICATIONS_KEY = "notifications";
    private readonly IDistributedCache _redisCache;
    private readonly ISerializeService _serializeService;
    private readonly ILogger _logger;

    public NotificationRepository(IDistributedCache redisCache, ISerializeService serializeService, ILogger logger)
    {
        _redisCache = redisCache;
        _serializeService = serializeService;
        _logger = logger;
    }
    public async Task<Entities.Notification> GetNotificationByIdAsync(Guid notificationId)
    {
        var notifications = await GetNotificationsAsync();
        return notifications.FirstOrDefault(n => n.Id == notificationId);
    }

    public async Task<IEnumerable<Entities.Notification>> GetNotificationsAsync()
    {
        var notifications = await _redisCache.GetStringAsync(NOTIFICATIONS_KEY);
        if (string.IsNullOrEmpty(notifications))
        {
            return Enumerable.Empty<Entities.Notification>();
        }
    
        try 
        {
            var result = _serializeService.Deserialize<List<Entities.Notification>>(notifications);
            return result ?? Enumerable.Empty<Entities.Notification>();
        }
        catch (Exception ex)
        {
            _logger.Error("Error deserializing notifications", ex);
            return Enumerable.Empty<Entities.Notification>();
        }
    }

    public async Task AddNotificationAsync(Entities.Notification notification, DistributedCacheEntryOptions options = null)
    {
        var notifications = (await GetNotificationsAsync()).ToList();
        notifications.Add(notification);
        var serializedData = _serializeService.Serialize(notifications);
        await _redisCache.SetStringAsync(NOTIFICATIONS_KEY, serializedData, options);
    }

    public async Task<bool> DeleteNotificationByIdAsync(Guid notificationId)
    {
        try
        {
            var notifications = (await GetNotificationsAsync()).ToList();
            
            var notificationToRemove = notifications.FirstOrDefault(n => n.Id == notificationId);
            if (notificationToRemove == null)
            {
                return false;
            }

            notifications.Remove(notificationToRemove);

            await _redisCache.SetStringAsync(
                NOTIFICATIONS_KEY, 
                _serializeService.Serialize(notifications)
            );

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"Failed to delete notification with id: {notificationId}", ex);
            throw;
        }
    }
}