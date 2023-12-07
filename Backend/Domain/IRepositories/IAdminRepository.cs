using System;

namespace IntelligentStore.Domain.IRepositories
{
    public interface IAdminRepository
    {
        public Task AddAsync(Admin admin);
        public Task<Admin> GetAsync(Guid id);
        public Task<Admin> GetAsync(string email);
    }
}
