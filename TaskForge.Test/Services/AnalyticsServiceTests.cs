using Application.Services;
using Domain;
using Domain.Interfaces.Repositories;
using Domain.Model;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskForge.Test
{
    public class AnalyticsServiceTests
    {
        private Mock<IProjectTaskRepository> _mockProjectTaskRepository;
        private Mock<ISprintRepository> _mockSprintRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ITimeLogRepository> _mockTimeLogRepository;
        private AnalyticsService _analyticsService;

        [SetUp]
        public void Setup()
        {
            _mockProjectTaskRepository = new Mock<IProjectTaskRepository>();
            _mockSprintRepository = new Mock<ISprintRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockTimeLogRepository = new Mock<ITimeLogRepository>();

            _analyticsService = new AnalyticsService(
                _mockProjectTaskRepository.Object,
                _mockSprintRepository.Object,
                _mockUserRepository.Object,
                _mockTimeLogRepository.Object);
        }

        [Test]
        public async Task AverageTasksCountPerSprint_NoSprints_ReturnsZero()
        {
            // Arrange
            var teamId = 1;
            _mockSprintRepository.Setup(repo => repo.GetTeamSprintsAsync(teamId))
                .ReturnsAsync((List<Sprint>)null);

            // Act
            var result = await _analyticsService.AverageTasksCountPerSprint(teamId);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task AverageTasksCountPerSprint_WithSprints_ReturnsCorrectAverage()
        {
            // Arrange
            var teamId = 1;
            var sprints = new List<Sprint>
            {
                new Sprint { ProjectTasks = new List<ProjectTask> { new ProjectTask(), new ProjectTask() } },
                new Sprint { ProjectTasks = new List<ProjectTask> { new ProjectTask() } },
                new Sprint { ProjectTasks = null } // No tasks
            };

            _mockSprintRepository.Setup(repo => repo.GetTeamSprintsAsync(teamId))
                .ReturnsAsync(sprints);

            // Act
            var result = await _analyticsService.AverageTasksCountPerSprint(teamId);

            // Assert
            Assert.AreEqual(1, result); // (2 + 1 + 0) / 3 = 1
        }

        [Test]
        public async Task AverageTaskCountPerUser_NoUsers_ReturnsZero()
        {
            // Arrange
            var teamId = 1;
            _mockUserRepository.Setup(repo => repo.GetTeamUsersAsync(teamId))
                .ReturnsAsync(new List<User>());

            // Act
            var result = await _analyticsService.AverageTaskCountPerUser(teamId);

            // Assert
            Assert.Throws<DivideByZeroException>(async () => await _analyticsService.AverageTaskCountPerUser(teamId));
        }

        [Test]
        public async Task AverageTaskCountPerUser_WithUsers_ReturnsCorrectAverage()
        {
            // Arrange
            var teamId = 1;
            var users = new List<User>
            {
                new User { ProjectTasks = new List<ProjectTask> { new ProjectTask { TeamId = teamId }, new ProjectTask { TeamId = teamId } } },
                new User { ProjectTasks = new List<ProjectTask> { new ProjectTask { TeamId = teamId } } }
            };

            _mockUserRepository.Setup(repo => repo.GetTeamUsersAsync(teamId))
                .ReturnsAsync(users);

            // Act
            var result = await _analyticsService.AverageTaskCountPerUser(teamId);

            // Assert
            Assert.AreEqual(1, result); // (2 + 1) / 2 = 1
        }

        [Test]
        public async Task AverageTimeForTask_NoCompletedTasks_ReturnsZero()
        {
            // Arrange
            var teamId = 1;
            var tasks = new List<ProjectTask> { new ProjectTask { TaskStatusId = 1 } }; // No completed tasks

            _mockProjectTaskRepository.Setup(repo => repo.GetAllInTeamAsync(teamId))
                .ReturnsAsync(tasks);

            // Act
            var result = await _analyticsService.AverageTimeForTask(teamId);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task AverageTimeForTask_WithCompletedTasks_ReturnsCorrectAverage()
        {
            // Arrange
            var teamId = 1;
            var completedTask = new ProjectTask { TaskId = 1, TaskStatusId = 2, CreatedAt = DateTime.UtcNow.AddDays(-3) };

            var tasks = new List<ProjectTask> { completedTask };

            var timeLog = new TimeLog { TaskId = 1, LogDate = DateTime.UtcNow };

            _mockProjectTaskRepository.Setup(repo => repo.GetAllInTeamAsync(teamId))
                .ReturnsAsync(tasks);

            _mockTimeLogRepository.Setup(repo => repo.GetTimeLogByDoneTaskIdAsync(completedTask.TaskId))
                .ReturnsAsync(timeLog);

            // Act
            var result = await _analyticsService.AverageTimeForTask(teamId);

            // Assert
            Assert.AreEqual(3, result, 0.1); // Average time should be ~3 days
        }
    }
}
