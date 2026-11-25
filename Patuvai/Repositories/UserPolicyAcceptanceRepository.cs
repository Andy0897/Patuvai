using Microsoft.EntityFrameworkCore;
using Patuvai.Data;
using Patuvai.Models;
using Patuvai.Repositories.Interfaces;

namespace Patuvai.Repositories
{
    public class UserPolicyAcceptanceRepository : IUserPolicyAcceptanceRepository
    {
        private readonly ApplicationDbContext _context;

        public UserPolicyAcceptanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserPolicyAcceptance> GetByIdAsync(int id)
        {
            return await _context.UserPolicyAcceptances.FindAsync(id);
        }

        public async Task<List<UserPolicyAcceptance>> GetAllAsync()
        {
            return await _context.UserPolicyAcceptances.ToListAsync();
        }

        public async Task AddAsync(UserPolicyAcceptance userPolicyAcceptance)
        {
            _context.UserPolicyAcceptances.Add(userPolicyAcceptance);
            await _context.SaveChangesAsync();
        }
    }
}
