using System;
using IntelligentStore.Infrastructure.EF.Context;
using Microsoft.AspNetCore.Identity;
using Shared;
using IntelligentStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace IntelligentStore.Infrastructure.EF
{
    public class AdminInitializer : IInitializer
    {
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminPassword = "!awdawd";
        private readonly IPasswordHasher<Admin> _passwordHasher;
        private readonly ApplicationDbContext _dbContext;

        public AdminInitializer(
            ApplicationDbContext dbContext,
            IPasswordHasher<Admin> passwordHasher
        )
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task InitAsync()
        {
            if (!await _dbContext.Admins.AnyAsync(q => q.Email == AdminEmail))
            {
                var password = _passwordHasher.HashPassword(default, AdminPassword);
                var user = Admin.Create(AdminEmail, password, DateTime.UtcNow);
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
