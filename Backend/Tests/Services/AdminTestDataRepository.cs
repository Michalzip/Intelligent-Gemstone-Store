using System;
using IntelligentStore.Domain;
using IntelligentStore.Domain.IRepositories;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace Tests.Services
{
    public class AdminTestDataRepository : IAdminRepository
    {
        private List<Admin> _admins;

        public AdminTestDataRepository()
        {
            var passwordHasher = new PasswordHasher<Admin>();

            _admins = new()
            {
                new Admin(
                    email: new Email("example@example.com"),
                    password: new Password(passwordHasher.HashPassword(default, "testPassword1")),
                    createdAt: DateTime.UtcNow
                ),
                new Admin(
                    email: new Email("another@example.com"),
                    password: new Password(passwordHasher.HashPassword(default, "testPassword1")),
                    createdAt: DateTime.UtcNow
                )
            };

            // var user2 = new User(
            //     email: new Email("another@example.com"),
            //     phoneNumber: new PhoneNumber("987654321"),
            //     address: new Address(
            //         userId: Guid.NewGuid(),
            //         street: new Street("Second Avenue"),
            //         houseNumber: new HouseNumber("456"),
            //         postalCode: new PostalCode("54321"),
            //         city: new City("New City")
            //     ),
            //     createdAt: DateTime.UtcNow
            // );
        }

        public Task AddAsync(Admin admin)
        {
            throw new NotImplementedException();
        }

        public async Task<Admin> GetAsync(Guid id)
        {
            return _admins.FirstOrDefault(e => e.Id == id);
        }

        public async Task<Admin> GetAsync(string email)
        {
            return _admins.FirstOrDefault(e => e.Email == email);
        }
    }
}
