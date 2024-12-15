using Application.Services;
using Domain;
using Domain.Interfaces.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[TestFixture]
public class ProjectTaskTypeServiceTests
{
    private Mock<ITaskTypeRepository> _taskTypeRepositoryMock;
    private ProjectTaskTypeService _service;

    [SetUp]
    public void SetUp()
    {
        _taskTypeRepositoryMock = new Mock<ITaskTypeRepository>();
        _service = new ProjectTaskTypeService(_taskTypeRepositoryMock.Object);
    }

    [Test]
    public async Task GetAllTaskTypesAsync_ReturnsAllTaskTypes()
    {
        // Arrange
        var taskTypes = new List<ProjectTaskType>
        {
            new ProjectTaskType { TypeId = 1, TypeName = "Bug" },
            new ProjectTaskType { TypeId = 2, TypeName = "Feature" }
        };
        _taskTypeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(taskTypes);

        // Act
        var result = await _service.GetAllTaskTypesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("Bug", result.First().TypeName);
    }

    [Test]
    public async Task GetTaskTypeByIdAsync_ReturnsCorrectTaskType()
    {
        // Arrange
        var taskType = new ProjectTaskType { TypeId = 1, TypeName = "Bug" };
        _taskTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(taskType);

        // Act
        var result = await _service.GetTaskTypeByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(1, result.TypeId);
        Assert.AreEqual("Bug", result.TypeName);
    }

    [Test]
    public async Task InsertTaskTypeAsync_CreatesNewTaskType()
    {
        // Arrange
        var taskType = new ProjectTaskType { TypeId = 1, TypeName = "Bug" };

        // Act
        await _service.InsertTaskTypeAsync(taskType);

        // Assert
        _taskTypeRepositoryMock.Verify(repo => repo.InsertAsync(taskType), Times.Once);
    }

    [Test]
    public async Task UpdateTaskTypeAsync_UpdatesExistingTaskType()
    {
        // Arrange
        var taskType = new ProjectTaskType { TypeId = 1, TypeName = "Bug" };

        // Act
        await _service.UpdateTaskTypeAsync(taskType);

        // Assert
        _taskTypeRepositoryMock.Verify(repo => repo.UpdateAsync(taskType), Times.Once);
    }

    [Test]
    public async Task DeleteTaskTypeAsync_DeletesTaskTypeById()
    {
        // Arrange
        var typeId = 1;

        // Act
        await _service.DeleteTaskTypeAsync(typeId);

        // Assert
        _taskTypeRepositoryMock.Verify(repo => repo.DeleteAsync(typeId), Times.Once);
    }
}
