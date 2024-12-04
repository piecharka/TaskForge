using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface INotficationRepository
    {
        Task<IEnumerable<Notification>> GetUsersNotificationsAsync(int userId);
        Task DeleteNotificationAsync(int notificationId);
        Task UpdateNotificationStatusAsync(int notificationId, int notificationStatusId);
    }
}
