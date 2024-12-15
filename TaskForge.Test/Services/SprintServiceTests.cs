using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Application.DTOs;
using Domain;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Model;

namespace Application.Tests
{
    [TestFixture]
    public class SprintServiceTests
    {
        private Mock<ISprintRepository> _mockSprintRepository;
        private Mock<ISprintEventRepository> _mockSprintEventRepository;
        private Mock<ITimeLogRepository> _mockTimeLogRepository;
        private Mock<IProjectTaskRepository> _mockProjectTaskRepository;
        private Mock<IMapper> _mockMapper;

        private SprintService _sprintService;

        [SetUp]
        public void SetUp()
        {
            // Mocking dependencies
            _mockSprintRepository = new Mock<ISprintRepository>();
            _mockSprintEventRepository = new Mock<ISprintEventRepository>();
            _mockTimeLogRepository = new Mock<ITimeLogRepository>();
            _mockProjectTaskRepository = new Mock<IProjectTaskRepository>();
            _mockMapper = new Mock<IMapper>();

            // Initialize SprintService with mocked dependencies
            _sprintService = new SprintService(
                _mockSprintRepository.Object,
                _mockMapper.Object,
                _mockSprintEventRepository.Object,
                _mockTimeLogRepository.Object,
                _mockProjectTaskRepository.Object
            );
        }

        [Test]
        public async Task GetSprintByIdAsync_ShouldReturnSprint_WhenSprintExists()
        {
            // Arrange
            var sprintId = 1;
            var expectedSprint = new Sprint { SprintId = sprintId };
            _mockSprintRepository.Setup(repo => repo.GetSprintByIdAsync(sprintId)).ReturnsAsync(expectedSprint);

            // Act
            var result = await _sprintService.GetSprintByIdAsync(sprintId);

            // Assert
            Assert.AreEqual(expectedSprint, result);
        }

        [Test]
        public async Task GetCurrentSprintTeamAsync_ShouldReturnSprintDto_WhenSprintExists()
        {
            // Arrange
            var teamId = 1;
            var sprint = new Sprint { SprintId = 1, SprintName = "Sprint 1", SprintStart = DateTime.Now, SprintEnd = DateTime.Now.AddDays(10) };
            var expectedSprintDto = new SprintDto { SprintId = sprint.SprintId, SprintName = sprint.SprintName };
            _mockSprintRepository.Setup(repo => repo.GetCurrentTeamSprintAsync(teamId)).ReturnsAsync(sprint);
            _mockMapper.Setup(mapper => mapper.Map<Sprint, SprintDto>(sprint)).Returns(expectedSprintDto);

            // Act
            var result = await _sprintService.GetCurrentSprintTeamAsync(teamId);

            // Assert
            Assert.AreEqual(expectedSprintDto, result);
        }

        [Test]
        public async Task GetSprintsAsync_ShouldReturnSprintDtos_WhenSprintsExist()
        {
            // Arrange
            var teamId = 1;
            var sprints = new List<Sprint>
            {
                new Sprint { SprintId = 1, SprintName = "Sprint 1", SprintStart = DateTime.Now, SprintEnd = DateTime.Now.AddDays(10) }
            };
            var expectedSprintDtos = new List<SprintDto>
            {
                new SprintDto { SprintId = 1, SprintName = "Sprint 1" }
            };
            _mockSprintRepository.Setup(repo => repo.GetTeamSprintsAsync(teamId)).ReturnsAsync(sprints);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<Sprint>, IEnumerable<SprintDto>>(sprints)).Returns(expectedSprintDtos);

            // Act
            var result = await _sprintService.GetSprintsAsync(teamId);

            // Assert
            Assert.AreEqual(expectedSprintDtos, result);
        }

        [Test]
        public async Task AddSprintAsync_ShouldAddSprintAndEvents_WhenValidSprintDto()
        {
            // Arrange
            var sprintDto = new SprintDto
            {
                SprintId = 1,
                SprintName = "Sprint 1",
                TeamId = 1,
                SprintStart = DateTime.Now,
                SprintEnd = DateTime.Now.AddDays(10),
                CreatedBy = 1
            };
            var sprint = new Sprint { SprintId = 1, SprintName = "Sprint 1", SprintStart = DateTime.Now, SprintEnd = DateTime.Now.AddDays(10) };
            _mockMapper.Setup(mapper => mapper.Map<Sprint>(sprintDto)).Returns(sprint);

            // Act
            await _sprintService.AddSprintAsync(sprintDto);

            // Assert
            _mockSprintRepository.Verify(repo => repo.AddSprintAsync(sprint), Times.Once);
            _mockSprintEventRepository.Verify(repo => repo.AddSprintEventAsync(It.IsAny<SprintEvent>()), Times.AtLeastOnce);
        }

        [Test]
        public async Task GetTaskCountPerSprintDayAsync_ShouldReturnTaskCountDtos_WhenValidSprintId()
        {
            // Arrange
            var sprintId = 1;
            var sprint = new Sprint
            {
                SprintId = sprintId,
                SprintStart = DateTime.Now,
                SprintEnd = DateTime.Now.AddDays(5)
            };
            var tasks = new List<ProjectTask>
            {
                new ProjectTask { TaskId = 1, SprintId = sprintId }
            };
            var timeLogs = new List<TimeLog>
            {
                new TimeLog { TaskId = 1, LogDate = DateTime.Now }
            };
            var taskCountDtos = new List<SprintTaskCountDto>
            {
                new SprintTaskCountDto { Day = DateTime.Now, TasksRemaining = 1 }
            };

            _mockSprintRepository.Setup(repo => repo.GetSprintByIdAsync(sprintId)).ReturnsAsync(sprint);
            _mockProjectTaskRepository.Setup(repo => repo.GetAllTasksBySprintIdAsync(sprintId)).ReturnsAsync(tasks);
            _mockTimeLogRepository.Setup(repo => repo.GetSprintTimeLogByDateAsync(It.IsAny<DateTime>(), sprintId, 2)).ReturnsAsync(timeLogs);
            _mockMapper.Setup(mapper => mapper.Map<ICollection<ProjectTask>, ICollection<ProjectTaskDto>>(tasks)).Returns(new List<ProjectTaskDto>());

            // Act
            var result = await _sprintService.GetTaskCountPerSprintDayAsync(sprintId);

            // Assert
            Assert.AreEqual(taskCountDtos.Count, result.Count);
        }

        [Test]
        public async Task DeleteSprintAsync_ShouldCallDelete_WhenSprintIdIsValid()
        {
            // Arrange
            var sprintId = 1;
            _mockSprintRepository.Setup(repo => repo.DeleteSprintAsync(sprintId)).Returns(Task.CompletedTask);

            // Act
            await _sprintService.DeleteSprintAsync(sprintId);

            // Assert
            _mockSprintRepository.Verify(repo => repo.DeleteSprintAsync(sprintId), Times.Once);
        }
    }
}
