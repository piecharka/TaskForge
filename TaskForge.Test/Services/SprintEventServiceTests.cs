using Application.Services;
using Application.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Model;
using Moq;
using NUnit.Framework;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Tests
{
    [TestFixture]
    public class SprintEventServiceTests
    {
        private Mock<ISprintEventRepository> _sprintEventRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private SprintEventService _sprintEventService;

        [SetUp]
        public void Setup()
        {
            _sprintEventRepositoryMock = new Mock<ISprintEventRepository>();
            _mapperMock = new Mock<IMapper>();
            _sprintEventService = new SprintEventService(_sprintEventRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetSprintEventsAsync_ShouldReturnAllSprintEvents()
        {
            // Arrange
            var sprintEvents = new List<SprintEvent>
            {
                new SprintEvent { SprintEventId = 1, SprintEventName = "Event 1" },
                new SprintEvent { SprintEventId = 2, SprintEventName = "Event 2" }
            };
            _sprintEventRepositoryMock.Setup(repo => repo.GetSprintEventsAsync())
                .ReturnsAsync(sprintEvents);

            // Act
            var result = await _sprintEventService.GetSprintEventsAsync();

            // Assert
            var resultList = result.ToList();  // Convert to List for indexing
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Event 1", resultList[0].SprintEventName);
            Assert.AreEqual("Event 2", resultList[1].SprintEventName);
        }

        [Test]
        public async Task GetSprintEventsByTeamIdAsync_ShouldReturnEventsByTeamId()
        {
            // Arrange
            var teamId = 1;
            var sprintEvents = new List<SprintEvent>
            {
                new SprintEvent { SprintEventId = 1, TeamId = teamId, SprintEventName = "Event 1" }
            };
            _sprintEventRepositoryMock.Setup(repo => repo.GetSprintEventsByTeamIdAsync(teamId))
                .ReturnsAsync(sprintEvents);

            // Act
            var result = await _sprintEventService.GetSprintEventsByTeamIdAsync(teamId);

            // Assert
            var resultList = result.ToList();  // Convert to List for indexing
            Assert.AreEqual(1, resultList.Count);
            Assert.AreEqual("Event 1", resultList[0].SprintEventName);
        }

        [Test]
        public async Task GetSprintEventByIdAsync_ShouldReturnEventById()
        {
            // Arrange
            var eventId = 1;
            var sprintEvent = new SprintEvent { SprintEventId = eventId, SprintEventName = "Event 1" };
            _sprintEventRepositoryMock.Setup(repo => repo.GetSprintEventByIdAsync(eventId))
                .ReturnsAsync(sprintEvent);

            // Act
            var result = await _sprintEventService.GetSprintEventByIdAsync(eventId);

            // Assert
            Assert.AreEqual(eventId, result.SprintEventId);
            Assert.AreEqual("Event 1", result.SprintEventName);
        }

        [Test]
        public async Task GetSprintEventsByUserIdAsync_ShouldReturnEventsByUserId()
        {
            // Arrange
            var userId = 1;
            var sprintEvents = new List<SprintEvent>
            {
                new SprintEvent { SprintEventId = 1, CreatedBy = userId, SprintEventName = "Event 1" }
            };
            _sprintEventRepositoryMock.Setup(repo => repo.GetSprintEventsByUserIdAsync(userId))
                .ReturnsAsync(sprintEvents);

            // Act
            var result = await _sprintEventService.GetSprintEventsByUserIdAsync(userId);

            // Assert
            var resultList = result.ToList();  // Convert to List for indexing
            Assert.AreEqual(1, resultList.Count);
            Assert.AreEqual("Event 1", resultList[0].SprintEventName);
        }

        [Test]
        public async Task GetClosestThreeEventsAsync_ShouldReturnClosestThreeEvents()
        {
            // Arrange
            var teamId = 1;
            var events = new List<SprintEvent>
            {
                new SprintEvent { SprintEventId = 1, TeamId = teamId, SprintEventDate = DateTime.Now.AddDays(1), SprintEventName = "Event 1" },
                new SprintEvent { SprintEventId = 2, TeamId = teamId, SprintEventDate = DateTime.Now.AddDays(5), SprintEventName = "Event 2" },
                new SprintEvent { SprintEventId = 3, TeamId = teamId, SprintEventDate = DateTime.Now.AddDays(2), SprintEventName = "Event 3" },
                new SprintEvent { SprintEventId = 4, TeamId = teamId, SprintEventDate = DateTime.Now.AddDays(10), SprintEventName = "Event 4" }
            };

            _sprintEventRepositoryMock.Setup(repo => repo.GetSprintEventsByTeamIdAsync(teamId))
                .ReturnsAsync(events);

            // Act
            var result = await _sprintEventService.GetClosestThreeEventsAsync(teamId);

            // Assert
            var resultList = result.ToList();  // Convert to List for indexing
            Assert.AreEqual(3, resultList.Count);

            // Sorting events to match the closest dates
            var sortedEvents = events.OrderBy(e => e.SprintEventDate).Take(3).ToList();

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(sortedEvents[i].SprintEventId, resultList[i].SprintEventId);
            }
        }

        [Test]
        public async Task AddSprintEventAsync_ShouldAddSprintEvent()
        {
            // Arrange
            var sprintEventDto = new SprintEventDto { SprintEventId = 1, SprintEventName = "New Event" };
            var sprintEvent = new SprintEvent { SprintEventId = 1, SprintEventName = "New Event" };
            _mapperMock.Setup(m => m.Map<SprintEventDto, SprintEvent>(sprintEventDto))
                .Returns(sprintEvent);
            _sprintEventRepositoryMock.Setup(repo => repo.AddSprintEventAsync(sprintEvent))
                .Returns(Task.CompletedTask);

            // Act
            await _sprintEventService.AddSprintEventAsync(sprintEventDto);

            // Assert
            _sprintEventRepositoryMock.Verify(repo => repo.AddSprintEventAsync(sprintEvent), Times.Once);
        }

        [Test]
        public async Task DeleteSprintEventAsync_ShouldDeleteSprintEvent()
        {
            // Arrange
            var sprintEventId = 1;
            _sprintEventRepositoryMock.Setup(repo => repo.DeleteSprintEventAsync(sprintEventId))
                .Returns(Task.CompletedTask);

            // Act
            await _sprintEventService.DeleteSprintEventAsync(sprintEventId);

            // Assert
            _sprintEventRepositoryMock.Verify(repo => repo.DeleteSprintEventAsync(sprintEventId), Times.Once);
        }
    }
}
