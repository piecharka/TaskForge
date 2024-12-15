using Application.Services;
using Domain;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace TaskForge.Test
{
    public class JwtTokenServiceTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private JwtTokenService _jwtTokenService;

        [SetUp]
        public void Setup()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(config => config["Jwt:Key"]).Returns("SuperSecretKey12345");
            _mockConfiguration.Setup(config => config["Jwt:Issuer"]).Returns("TestIssuer");
            _mockConfiguration.Setup(config => config["Jwt:Audience"]).Returns("TestAudience");

            _jwtTokenService = new JwtTokenService(_mockConfiguration.Object);
        }

        [Test]
        public void GenerateToken_ReturnsValidJwtToken()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                Username = "TestUser",
                Email = "testuser@example.com"
            };

            // Act
            var token = _jwtTokenService.GenerateToken(user);

            // Assert
            Assert.IsNotNull(token);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.AreEqual("TestIssuer", jwtToken.Issuer);
            Assert.AreEqual("TestAudience", jwtToken.Audiences.First());

            var subjectClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            Assert.IsNotNull(subjectClaim);
            Assert.AreEqual(user.Username, subjectClaim.Value);

            var nameIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            Assert.IsNotNull(nameIdClaim);
            Assert.AreEqual(user.UserId.ToString(), nameIdClaim.Value);
        }

        [Test]
        public void GenerateToken_ThrowsExceptionForInvalidConfiguration()
        {
            // Arrange
            _mockConfiguration.Setup(config => config["Jwt:Key"]).Returns(string.Empty);

            var user = new User
            {
                UserId = 1,
                Username = "TestUser",
                Email = "testuser@example.com"
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _jwtTokenService.GenerateToken(user));
        }

        [Test]
        public void GenerateToken_ValidatesExpirationTime()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                Username = "TestUser",
                Email = "testuser@example.com"
            };

            // Act
            var token = _jwtTokenService.GenerateToken(user);

            // Assert
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var expirationClaim = jwtToken.ValidTo;
            Assert.IsTrue(expirationClaim > DateTime.UtcNow);
            Assert.IsTrue(expirationClaim <= DateTime.UtcNow.AddMinutes(30));
        }
    }
}
