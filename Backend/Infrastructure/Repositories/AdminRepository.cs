using IntelligentStore.Domain;
using IntelligentStore.Domain.IRepositories;
using IntelligentStore.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace IntelligentStore.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Admin> _admins;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Admin admin)
        {
            await _admins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<Admin> GetAsync(Guid id) =>
            await _admins.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Admin> GetAsync(string email) =>
            await _context.Admins.SingleOrDefaultAsync(x => x.Email == email);
    }
}
