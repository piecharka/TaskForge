using Application.DTOs;
using Application.Interfaces.Services;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUsersNotification(int userId)
        {
            return Ok(await _notificationService.GetUsersNotificationsAsync(userId));
        }

        [HttpDelete("{notificationId}")]
        public async Task<ActionResult> DeleteNotification(int notificationId)
        {
            await _notificationService.DeleteNotificationAsync(notificationId);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateNotificationStatus(int notificationId, int statusId)
        {
            await _notificationService.UpdateNotificationStatusAsync(notificationId, statusId);

            return NoContent();
        }
    }
}
