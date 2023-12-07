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

        //? w edytorze jest opcja do testow wiec poprstu wybieram sobie np ta funckje do testowania
        //! tutaj odnosnik co dalej..

        //https://github.com/KevinDockx/UnitTestingAspNetCore6WebAPI/blob/cf84a8e3fdfd2644251b3cf30f951f8885b76f7a/Finished%20solution/EmployeeManagement.Test/MoqTests.cs
        [Fact]
        public void InsertData_IntoAdminTestDataRepository_AndRetrieveExpectedResult()
        {
            // Arrange
            var user = _adminServiceFixture.adminRepository;

            // ACT
            //USER.DODAJ DANE

            // Assert

            //ustaw oczekiwania
        }
    }
}
