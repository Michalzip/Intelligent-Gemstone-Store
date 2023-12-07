using System;
using System.Net;
using IntelligentStore.Domain.IRepositories;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace IntelligentStore.Domain.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;
        private readonly IPasswordHasher<Admin> _passwordHasher;

        public AdminService(IAdminRepository adminRepository, IPasswordHasher<Admin> passwordHasher)
        {
            _adminRepository = adminRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Admin> CheckCredentials(string email, string password)
        {
            var admin = await GetAdminByEmail(email);
            VerifyPassword(admin.Password, password);
            return admin;
        }

        private async Task<Admin> GetAdminByEmail(string email)
        {
            var admin = await _adminRepository.GetAsync(email);

            if (admin == null)
                throw new InvalidCredentialsException();

            return admin;
        }

        private void VerifyPassword(string userPassword, string password)
        {
            if (
                _passwordHasher.VerifyHashedPassword(default, userPassword, password)
                == PasswordVerificationResult.Failed
            )
            {
                throw new InvalidCredentialsException();
            }
        }
    }
}
