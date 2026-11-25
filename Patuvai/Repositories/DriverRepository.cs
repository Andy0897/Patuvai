using Microsoft.EntityFrameworkCore;
using Patuvai.Data;
using Patuvai.Models;
using Patuvai.Repositories.Interfaces;

namespace Patuvai.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ApplicationDbContext _context;

        public DriverRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Driver?> GetByIdAsync(int id)
        {
            return await _context.Drivers
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Driver>> GetAllAsync()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task AddAsync(Driver driver)
        {
            await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver != null)
            {
                _context.Drivers.Remove(driver);
                await _context.SaveChangesAsync();
            }
        }
    }
}
