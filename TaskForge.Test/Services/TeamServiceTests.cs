using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Application.DTOs;
using Domain.Model;
using Domain.Interfaces.Repositories;
using AutoMapper;

namespace Application.Tests
{
    [TestFixture]
    public class TeamServiceTests
    {
        private Mock<ITeamRepository> _mockTeamRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IPermissionRepository> _mockPermissionRepository;
        private Mock<IMapper> _mockMapper;
        private TeamService _teamService;

        [SetUp]
        public void SetUp()
        {
            // Mock repositories
            _mockTeamRepository = new Mock<ITeamRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPermissionRepository = new Mock<IPermissionRepository>();
            _mockMapper = new Mock<IMapper>(); 

            // Initialize TeamService with mocked dependencies
            _teamService = new TeamService(
                _mockTeamRepository.Object,
                _mockMapper.Object,
                _mockUserRepository.Object,
                _mockPermissionRepository.Object);
        }

    }
}
