using Microsoft.EntityFrameworkCore;
using Patuvai.Data;
using Patuvai.Models;
using Patuvai.Models.Enums;
using Patuvai.Repositories.Interfaces;

namespace Patuvai.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly ApplicationDbContext _context;

        public PolicyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Policy> GetByIdAsync(int id)
        {
            return await _context.Policies.FindAsync(id);
        }

        public async Task<List<Policy>> GetAllAsync()
        {
            return await _context.Policies.ToListAsync();
        }

        public async Task<Policy> GetActiveVersionAsync(PolicyType type)
        {
            return await _context.Policies
                .Where(p => p.Type == type && p.IsActive)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Policy policy)
        {
            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Policy policy)
        {
            _context.Policies.Update(policy);
            await _context.SaveChangesAsync();
        }
    }
}
