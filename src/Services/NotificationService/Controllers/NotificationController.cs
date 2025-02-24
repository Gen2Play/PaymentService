using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NotificationService.Repositories.Interfaces;
using Shared.DTOs;
using ILogger = Serilog.ILogger;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly INotificationRepository _repository;

        public NotificationController(ILogger logger, INotificationRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<ResponseDto<IEnumerable<Entities.Notification>>>> GetNotifications()
        {
            try
            {
                _logger.Information("Start fetching all notifications");

                var notifications = await _repository.GetNotificationsAsync();
                _logger.Information($"Successfully retrieved {notifications?.Count() ?? 0} notifications");

                return Ok(new ResponseDto<IEnumerable<Entities.Notification>>(
                    data: notifications,
                    message: "Notifications retrieved successfully",
                    status: StatusCodes.Status200OK
                ));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while fetching notifications. Error: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto<IEnumerable<Entities.Notification>>(
                        data: null,
                        message: $"An error occurred while processing your request: {ex.Message}",
                        status: StatusCodes.Status500InternalServerError
                    ));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<Entities.Notification>>> GetNotificationById(Guid id)
        {
            try
            {
                _logger.Information($"Start fetching notification with id: {id}");

                var notification = await _repository.GetNotificationByIdAsync(id);

                if (notification == null)
                {
                    _logger.Information($"Notification not found with id: {id}");
                    return NotFound(new ResponseDto<Entities.Notification>(
                        data: null,
                        message: $"Notification not found with id: {id}",
                        status: StatusCodes.Status404NotFound
                    ));
                }

                _logger.Information($"Successfully retrieved notification with id: {id}");
                return Ok(new ResponseDto<Entities.Notification>(
                    data: notification,
                    message: "Notification retrieved successfully",
                    status: StatusCodes.Status200OK
                ));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while fetching notification with id: {id}. Error: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto<Entities.Notification>(
                        data: null,
                        message: $"An error occurred while processing your request: {ex.Message}",
                        status: StatusCodes.Status500InternalServerError
                    ));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<Entities.Notification>>> AddNotification([FromBody] Entities.Notification notification)
        {
            try
            {
                _logger.Information($"Start adding new notification with id: {notification.Id}");

                var cacheOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = null,
                    SlidingExpiration = null
                };

                await _repository.AddNotificationAsync(notification, cacheOptions);
                _logger.Information($"Successfully added notification with id: {notification.Id}");

                return Ok(new ResponseDto<Entities.Notification>(
                    data: notification,
                    message: "Notification added successfully",
                    status: StatusCodes.Status200OK
                ));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while adding notification. Error: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto<Entities.Notification>(
                        data: null,
                        message: $"An error occurred while processing your request: {ex.Message}",
                        status: StatusCodes.Status500InternalServerError
                    ));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteNotification(Guid id)
        {
            try
            {
                _logger.Information($"Start deleting notification with id: {id}");

                var isDeleted = await _repository.DeleteNotificationByIdAsync(id);

                if (!isDeleted)
                {
                    _logger.Information($"Notification not found with id: {id}");
                    return NotFound(new ResponseDto<bool>(
                        data: false,
                        message: $"Notification not found with id: {id}",
                        status: StatusCodes.Status404NotFound
                    ));
                }

                _logger.Information($"Successfully deleted notification with id: {id}");
                return Ok(new ResponseDto<bool>(
                    data: true,
                    message: "Notification deleted successfully",
                    status: StatusCodes.Status200OK
                ));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while deleting notification with id: {id}. Error: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto<bool>(
                        data: false,
                        message: $"An error occurred while processing your request: {ex.Message}",
                        status: StatusCodes.Status500InternalServerError
                    ));
            }
        }
    }
}