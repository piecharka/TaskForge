using Application;
using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain;
using Domain.Interfaces.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[TestFixture]
public class ProjectTaskServiceTests
{
    private Mock<IProjectTaskRepository> _projectTaskRepositoryMock;
    private Mock<ICommentRepository> _commentRepositoryMock;
    private Mock<IUsersTaskRepository> _usersTaskRepositoryMock;
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<ITimeLogRepository> _timeLogRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private ProjectTaskService _service;

    [SetUp]
    public void SetUp()
    {
        _projectTaskRepositoryMock = new Mock<IProjectTaskRepository>();
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _usersTaskRepositoryMock = new Mock<IUsersTaskRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _timeLogRepositoryMock = new Mock<ITimeLogRepository>();
        _mapperMock = new Mock<IMapper>();

        _service = new ProjectTaskService(
            _projectTaskRepositoryMock.Object,
            _mapperMock.Object,
            _commentRepositoryMock.Object,
            _usersTaskRepositoryMock.Object,
            _userRepositoryMock.Object,
            _timeLogRepositoryMock.Object
        );
    }

    [Test]
    public async Task GetAllProjectTasksInTeamAsync_ReturnsMappedTasks()
    {
        // Arrange
        var teamId = 1;
        var sortParams = new SortParams { SortBy = "id", SortOrder = "asc" };
        var filters = new Dictionary<string, string>();

        var tasks = new List<ProjectTask> { new ProjectTask { TaskId = 1, TaskName = "Task 1" } };
        _projectTaskRepositoryMock.Setup(repo => repo.GetAllInTeamAsync(teamId)).ReturnsAsync(tasks.AsQueryable());

        var taskDtos = new List<ProjectTaskDto> { new ProjectTaskDto { TaskId = 1, TaskName = "Task 1" } };
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ProjectTask>, IEnumerable<ProjectTaskDto>>(tasks)).Returns(taskDtos);

        // Act
        var result = await _service.GetAllProjectTasksInTeamAsync(teamId, sortParams, filters);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(taskDtos.First().TaskId, result.First().TaskId);
    }

    [Test]
    public async Task AddProjectTaskAsync_CreatesTask()
    {
        // Arrange
        var projectTaskInsertDto = new ProjectTaskInsertDto { TaskName = "New Task", UserIds = new List<int> { 1, 2 } };
        var projectTask = new ProjectTask { TaskName = "New Task" };

        _mapperMock.Setup(mapper => mapper.Map<ProjectTaskInsertDto, ProjectTask>(projectTaskInsertDto)).Returns(projectTask);

        // Act
        await _service.AddProjectTaskAsync(projectTaskInsertDto);

        // Assert
        _projectTaskRepositoryMock.Verify(repo => repo.InsertAsync(projectTask, projectTaskInsertDto.UserIds), Times.Once);
    }

    [Test]
    public async Task DeleteProjectTaskAsync_RemovesTaskAndRelatedData()
    {
        // Arrange
        var taskId = 1;
        var comments = new List<Comment> { new Comment { CommentId = 1 } };
        var usersTasks = new List<UsersTask> { new UsersTask { UserTaskId = 1 } };

        _commentRepositoryMock.Setup(repo => repo.GetAllTaskCommentsAsync(taskId)).ReturnsAsync(comments);
        _usersTaskRepositoryMock.Setup(repo => repo.GetUsersTaskByTaskIdAsync(taskId)).ReturnsAsync(usersTasks);

        // Act
        await _service.DeleteProjectTaskAsync(taskId);

        // Assert
        _commentRepositoryMock.Verify(repo => repo.DeleteAsync(comments.First().CommentId), Times.Once);
        _usersTaskRepositoryMock.Verify(repo => repo.DeleteUserTaskByIdAsync(usersTasks.First().UserTaskId), Times.Once);
        _projectTaskRepositoryMock.Verify(repo => repo.DeleteAsync(taskId), Times.Once);
    }

    [Test]
    public async Task GetTasksBySprintIdAsync_ReturnsMappedTasks()
    {
        // Arrange
        var sprintId = 1;
        var tasks = new List<ProjectTask> { new ProjectTask { TaskId = 1, TaskName = "Task 1" } };
        _projectTaskRepositoryMock.Setup(repo => repo.GetAllTasksBySprintIdAsync(sprintId)).ReturnsAsync(tasks);

        var taskDtos = new List<ProjectTaskDto> { new ProjectTaskDto { TaskId = 1, TaskName = "Task 1" } };
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ProjectTask>, IEnumerable<ProjectTaskDto>>(tasks)).Returns(taskDtos);

        // Act
        var result = await _service.GetTasksBySprintIdAsync(sprintId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(taskDtos.First().TaskId, result.First().TaskId);
    }

    [Test]
    public async Task UpdateProjectTaskStatusAsync_AddsTimeLog_WhenStatusIsDone()
    {
        // Arrange
        var taskId = 1;
        var statusId = 2; // Done status

        // Act
        await _service.UpdateProjectTaskStatusAsync(taskId, statusId);

        // Assert
        _projectTaskRepositoryMock.Verify(repo => repo.UpdateProjectTaskStatusAsync(taskId, statusId), Times.Once);
        _timeLogRepositoryMock.Verify(repo => repo.AddTimeLog(It.Is<TimeLog>(log => log.TaskId == taskId && log.LogTypeId == 2)), Times.Once);
    }
}
