using Domain;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class NotificationRepository : INotficationRepository
    {
        private readonly TaskForgeDbContext _taskForgeDbContext;
        public NotificationRepository(TaskForgeDbContext context) 
        {
            _taskForgeDbContext = context;
        }

        public async Task<IEnumerable<Notification>> GetUsersNotificationsAsync(int userId)
        {
            return await _taskForgeDbContext.Notifications
                .Include(n => n.NotificationStatus)
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _taskForgeDbContext.Notifications
                .Where(n => n.NotificationId == notificationId)
                .FirstOrDefaultAsync();

            if (notification != null) 
            {
                _taskForgeDbContext.Notifications.Remove(notification);
                await _taskForgeDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Notification with id {notificationId} not found.");
            }
        }

        public async Task UpdateNotificationStatusAsync(int notificationId, int notificationStatusId)
        {
            var notification = await _taskForgeDbContext.Notifications
                .Where(n => n.NotificationId == notificationId)
                .FirstOrDefaultAsync();

            if (notification != null)
            {
                notification.NotificationStatusId = notificationStatusId;

                _taskForgeDbContext.Notifications
                    .Update(notification);
                await _taskForgeDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Notification with id {notificationId} not found.");
            }
        }
    }
}
