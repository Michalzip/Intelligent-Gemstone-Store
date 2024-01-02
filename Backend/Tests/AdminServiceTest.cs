using System;
using IntelligentStore.Domain.IRepositories;
using Xunit;
using Tests.Services;
using Tests.Fixtures;

namespace Tests
{
    public class AdminServiceTest
    {
        public IAdminRepository _adminRepository;
        private readonly AdminServiceFixture _adminServiceFixture;

        public AdminServiceTest(AdminServiceFixture adminServiceFixture)
        {
            _adminServiceFixture = adminServiceFixture;
        }

        [Fact]
        public async Task InsertData_IntoAdminTestDataRepository_AndRetrieveExpectedResult()
        {
            // Arrange
            var userEmail = "example@example.com";

            // ACT
            var user = await _adminServiceFixture.adminRepository.GetAsync("example@example.com");

            // Assert
            Assert.NotNull(user);
        }
    }
}
