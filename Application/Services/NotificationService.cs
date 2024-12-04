using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotficationRepository _notficationRepository;
        private readonly IMapper _mapper;

        public NotificationService(IMapper mapper, INotficationRepository notficationRepository)
        {
            _notficationRepository = notficationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationDto>> GetUsersNotificationsAsync(int userId)
        {
            var notfication = await _notficationRepository.GetUsersNotificationsAsync(userId);

            return _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDto>>(notfication);
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            await _notficationRepository.DeleteNotificationAsync(notificationId);
        }

        public async Task UpdateNotificationStatusAsync(int notificationId, int statusId)
        {
            await _notficationRepository.UpdateNotificationStatusAsync(notificationId, statusId);
        }
    }
}
