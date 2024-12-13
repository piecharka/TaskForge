

namespace Domain.Interfaces.Repositories
{
    public interface INotficationRepository
    {
        Task<IEnumerable<Notification>> GetUsersNotificationsAsync(int userId);
        Task DeleteNotificationAsync(int notificationId);
        Task UpdateNotificationStatusAsync(int notificationId, int notificationStatusId);
    }
}
