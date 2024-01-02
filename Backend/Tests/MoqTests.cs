using System;
using System.Net;
using IntelligentStore.Domain;
using IntelligentStore.Domain.Services.AdminService;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shared;
using Tests.Services;
using Xunit;

namespace Tests
{
    public class MoqTests
    {
        [Fact]
        public async Task CHECK_BEHAVIOR_FOR_INVALID_ADMIN_CREDENTIAL()
        {
            // Arrange
            var employeeManagementTestDataRepository = new AdminTestDataRepository();
            var passwordHasherMock = new Mock<IPasswordHasher<Admin>>();

            var adminService = new AdminService(
                employeeManagementTestDataRepository,
                passwordHasherMock.Object
            );

            // Act
            var exception = await Assert.ThrowsAsync<InvalidCredentialsException>(async () =>
            {
                var admin = await adminService.CheckCredentials(
                    "another@example.com",
                    "testPassword1"
                );
            });

            // Assert
            Assert.NotNull(exception);
            Assert.Contains("Invalid Credentials", exception.Message);
        }
    }
}
