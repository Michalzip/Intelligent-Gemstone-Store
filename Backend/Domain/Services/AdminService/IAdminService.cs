using System;

namespace IntelligentStore.Domain.Services.AdminService
{
    public interface IAdminService
    {
        public Task<Admin> CheckCredentials(string email, string password);
    }
}
