using Microsoft.EntityFrameworkCore;
using Patuvai.Data;
using Patuvai.Models;
using Patuvai.Repositories.Interfaces;

namespace Patuvai.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _context;

        public ConversationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation?> GetByIdAsync(int id)
        {
            return await _context.Conversations
                .Include(c => c.Messages)
                .Include(c => c.Listing)
                .Include(c => c.ListingOwner)
                .Include(c => c.ListingUser)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Conversation>> GetAllAsync()
        {
            return await _context.Conversations.ToListAsync();
        }

        public async Task AddAsync(Conversation conversation)
        {
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Conversation conversation)
        {
            _context.Conversations.Update(conversation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var conversation = await _context.Conversations.FindAsync(id);
            if (conversation != null)
            {
                _context.Conversations.Remove(conversation);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Conversation not found");
            }
        }

        public async Task<bool> CheckIfExistsAsync(int userId, int listingId)
        {
            return await _context.Conversations
                .AnyAsync(c => c.ListingUserId == userId && c.ListingId == listingId);
        }
    }
}
