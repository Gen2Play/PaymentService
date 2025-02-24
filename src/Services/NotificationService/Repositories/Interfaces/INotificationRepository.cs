using Microsoft.Extensions.Caching.Distributed;

namespace NotificationService.Repositories.Interfaces;

public interface INotificationRepository
{
    Task<Entities.Notification> GetNotificationByIdAsync(Guid notificationId);
    Task<IEnumerable<Entities.Notification>> GetNotificationsAsync();
    Task AddNotificationAsync(Entities.Notification notification, DistributedCacheEntryOptions options = null);
    Task<bool> DeleteNotificationByIdAsync(Guid notificationId);
}