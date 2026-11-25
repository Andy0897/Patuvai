using Microsoft.EntityFrameworkCore;
using Patuvai.Data;
using Patuvai.Models;
using Patuvai.Repositories.Interfaces;

namespace Patuvai.Repositories
{
    public class RequestListingRepository : IRequestListingRepository
    {
        private readonly ApplicationDbContext _context;
        public RequestListingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RequestListing?> GetByIdAsync(int id)
        {
            return await _context.RequestListings
                .Include(r => r.FromCity)
                .Include(r => r.ToCity)
                .Include(r => r.Publisher)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<List<RequestListing>> GetAllAsync()
        {
            return await _context.RequestListings
                .Include(r => r.FromCity)
                .Include(r => r.ToCity)
                .Include(r => r.Publisher)
                .ToListAsync();
        }
        public async Task AddAsync(RequestListing requestListing)
        {
            _context.RequestListings.Add(requestListing);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(RequestListing requestListing)
        {
            _context.RequestListings.Update(requestListing);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var requestListing = await _context.RequestListings.FindAsync(id);
            if (requestListing != null)
            {
                _context.RequestListings.Remove(requestListing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
