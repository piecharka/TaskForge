using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain;
using Domain.Interfaces.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskForge.Test
{
    public class NotificationServiceTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<INotficationRepository> _mockNotificationRepository;
        private NotificationService _notificationService;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _mockNotificationRepository = new Mock<INotficationRepository>();
            _notificationService = new NotificationService(_mockMapper.Object, _mockNotificationRepository.Object);
        }

        [Test]
        public async Task GetUsersNotificationsAsync_ReturnsMappedNotifications()
        {
            // Arrange
            var userId = 1;
            var notifications = new List<Notification>
            {
                new Notification { NotificationId = 1, UserId = userId, Message = "Test Message 1" },
                new Notification { NotificationId = 2, UserId = userId, Message = "Test Message 2" }
            };

            var notificationDtos = new List<NotificationDto>
            {
                new NotificationDto { NotificationId = 1, UserId = userId, Message = "Test Message 1" },
                new NotificationDto { NotificationId = 2, UserId = userId, Message = "Test Message 2" }
            };

            _mockNotificationRepository.Setup(repo => repo.GetUsersNotificationsAsync(userId))
                .ReturnsAsync(notifications);

            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDto>>(notifications))
                .Returns(notificationDtos);

            // Act
            var result = await _notificationService.GetUsersNotificationsAsync(userId);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(notificationDtos, result);
        }

        [Test]
        public async Task DeleteNotificationAsync_DeletesNotification()
        {
            // Arrange
            var notificationId = 1;

            _mockNotificationRepository.Setup(repo => repo.DeleteNotificationAsync(notificationId))
                .Returns(Task.CompletedTask);

            // Act
            await _notificationService.DeleteNotificationAsync(notificationId);

            // Assert
            _mockNotificationRepository.Verify(repo => repo.DeleteNotificationAsync(notificationId), Times.Once);
        }

        [Test]
        public async Task UpdateNotificationStatusAsync_UpdatesStatus()
        {
            // Arrange
            var notificationId = 1;
            var statusId = 2;

            _mockNotificationRepository.Setup(repo => repo.UpdateNotificationStatusAsync(notificationId, statusId))
                .Returns(Task.CompletedTask);

            // Act
            await _notificationService.UpdateNotificationStatusAsync(notificationId, statusId);

            // Assert
            _mockNotificationRepository.Verify(repo => repo.UpdateNotificationStatusAsync(notificationId, statusId), Times.Once);
        }
    }
}
