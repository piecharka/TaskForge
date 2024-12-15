using Application.Services;
using Domain.Interfaces.Repositories;
using Domain.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskForge.Test
{
    public class PermissionServiceTests
    {
        private Mock<IPermissionRepository> _mockPermissionRepository;
        private PermissionService _permissionService;

        [SetUp]
        public void Setup()
        {
            _mockPermissionRepository = new Mock<IPermissionRepository>();
            _permissionService = new PermissionService(_mockPermissionRepository.Object);
        }

        [Test]
        public async Task GetPermissionByIdAsync_ReturnsCorrectPermission()
        {
            // Arrange
            var permissionId = 1;
            var permission = new Permission { PermissionId = permissionId, PermissionName = "Test Permission", PermissionRank = 10 };

            _mockPermissionRepository.Setup(repo => repo.GetByIdAsync(permissionId))
                .ReturnsAsync(permission);

            // Act
            var result = await _permissionService.GetPermissionByIdAsync(permissionId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(permissionId, result.PermissionId);
            Assert.AreEqual("Test Permission", result.PermissionName);
        }

        [Test]
        public async Task GetPermissionsAsync_ReturnsAllPermissions()
        {
            // Arrange
            var permissions = new List<Permission>
            {
                new Permission { PermissionId = 1, PermissionName = "Permission 1", PermissionRank = 1 },
                new Permission { PermissionId = 2, PermissionName = "Permission 2", PermissionRank = 2 }
            };

            _mockPermissionRepository.Setup(repo => repo.GetPermissionsAsync())
                .ReturnsAsync(permissions);

            // Act
            var result = await _permissionService.GetPermissionsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetPermissionByUserIdAsync_ReturnsUserPermission()
        {
            // Arrange
            var userId = 1;
            var teamId = 2;
            var permission = new Permission { PermissionId = 1, PermissionName = "User Permission", PermissionRank = 5 };

            _mockPermissionRepository.Setup(repo => repo.GetByUserIdAsync(userId, teamId))
                .ReturnsAsync(permission);

            // Act
            var result = await _permissionService.GetPermissionByUserIdAsync(userId, teamId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(permission.PermissionId, result.PermissionId);
            Assert.AreEqual("User Permission", result.PermissionName);
        }

        [Test]
        public async Task UpdateUsersPermissionAsync_UpdatesPermissionSuccessfully()
        {
            // Arrange
            var userId = 1;
            var teamId = 2;
            var permissionId = 3;

            _mockPermissionRepository.Setup(repo => repo.ChangeUsersPermissionAsync(userId, teamId, permissionId))
                .ReturnsAsync(true);

            // Act
            var result = await _permissionService.UpdateUsersPermissionAsync(userId, teamId, permissionId);

            // Assert
            Assert.IsTrue(result);
            _mockPermissionRepository.Verify(repo => repo.ChangeUsersPermissionAsync(userId, teamId, permissionId), Times.Once);
        }
    }
}
