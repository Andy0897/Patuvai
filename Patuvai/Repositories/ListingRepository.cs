using Microsoft.EntityFrameworkCore;
using Patuvai.Data;
using Patuvai.Models;
using Patuvai.Repositories.Interfaces;

namespace Patuvai.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Listing?> GetByIdAsync(int id)
        {
            return await _context.Listings.FindAsync(id);
        }

        public async Task<List<Listing>> GetAllAsync()
        {
            return await _context.Listings.ToListAsync();
        }
    }
}
