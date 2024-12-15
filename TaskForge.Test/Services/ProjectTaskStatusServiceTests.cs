using Application.Services;
using Domain;
using Domain.Interfaces.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[TestFixture]
public class ProjectTaskStatusServiceTests
{
    private Mock<ITaskStatusRepository> _taskStatusRepositoryMock;
    private ProjectTaskStatusService _service;

    [SetUp]
    public void SetUp()
    {
        _taskStatusRepositoryMock = new Mock<ITaskStatusRepository>();
        _service = new ProjectTaskStatusService(_taskStatusRepositoryMock.Object);
    }

    [Test]
    public async Task GetAllTaskStatusAsync_ReturnsAllStatuses()
    {
        // Arrange
        var statuses = new List<ProjectTaskStatus>
        {
            new ProjectTaskStatus { StatusId = 1, StatusName = "To Do" },
            new ProjectTaskStatus { StatusId = 2, StatusName = "In Progress" }
        };
        _taskStatusRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(statuses);

        // Act
        var result = await _service.GetAllTaskStatusAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("To Do", result.First().StatusName);
    }

    [Test]
    public async Task GetTaskStatusByIdAsync_ReturnsCorrectStatus()
    {
        // Arrange
        var statusId = 1;
        var status = new ProjectTaskStatus { StatusId = 1, StatusName = "To Do" };
        _taskStatusRepositoryMock.Setup(repo => repo.GetByIdAsync(statusId)).ReturnsAsync(status);

        // Act
        var result = await _service.GetTaskStatusByIdAsync(statusId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(statusId, result.StatusId);
        Assert.AreEqual("To Do", result.StatusName);
    }

    [Test]
    public async Task InsertTaskStatusAsync_AddsNewStatus()
    {
        // Arrange
        var newStatus = new ProjectTaskStatus { StatusId = 3, StatusName = "Completed" };

        // Act
        await _service.InsertTaskStatusAsync(newStatus);

        // Assert
        _taskStatusRepositoryMock.Verify(repo => repo.InsertAsync(newStatus), Times.Once);
    }

    [Test]
    public async Task UpdateTaskStatusAsync_UpdatesStatus()
    {
        // Arrange
        var updatedStatus = new ProjectTaskStatus { StatusId = 1, StatusName = "To Do Updated" };

        // Act
        await _service.UpdateTaskStatusAsync(updatedStatus);

        // Assert
        _taskStatusRepositoryMock.Verify(repo => repo.UpdateAsync(updatedStatus), Times.Once);
    }

    [Test]
    public async Task DeleteTaskStatusAsync_RemovesStatus()
    {
        // Arrange
        var statusId = 1;

        // Act
        await _service.DeleteTaskStatusAsync(statusId);

        // Assert
        _taskStatusRepositoryMock.Verify(repo => repo.DeleteAsync(statusId), Times.Once);
    }
}
