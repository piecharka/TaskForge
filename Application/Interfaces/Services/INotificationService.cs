using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetUsersNotificationsAsync(int userId);
        Task DeleteNotificationAsync(int notificationId);
        Task UpdateNotificationStatusAsync(int notificationId, int statusId);
    }
}
