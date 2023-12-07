using System;
using IntelligentStore.Domain;
using IntelligentStore.Domain.IRepositories;
using Shared;

namespace Tests.Services
{
    public class AdminTestDataRepository : IAdminRepository
    {
        private List<User> _users;

        public AdminTestDataRepository()
        {
            // mimic expensive creation process
            Thread.Sleep(3000);

            var user1 = new User(
                email: new Email("example@example.com"),
                phoneNumber: new PhoneNumber("123456789"),
                address: new Address(
                    userId: Guid.NewGuid(),
                    street: new Street("Main Street"),
                    houseNumber: new HouseNumber("123"),
                    postalCode: new PostalCode("12345"),
                    city: new City("Example City")
                ),
                createdAt: DateTime.UtcNow
            );

            var user2 = new User(
                email: new Email("another@example.com"),
                phoneNumber: new PhoneNumber("987654321"),
                address: new Address(
                    userId: Guid.NewGuid(),
                    street: new Street("Second Avenue"),
                    houseNumber: new HouseNumber("456"),
                    postalCode: new PostalCode("54321"),
                    city: new City("New City")
                ),
                createdAt: DateTime.UtcNow
            );

            _users = new() { user1, user2 };
        }

        public Task AddAsync(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Task<Admin> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Admin> GetAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
