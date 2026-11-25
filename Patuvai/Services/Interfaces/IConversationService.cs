using Patuvai.Models;

namespace Patuvai.Services.Interfaces
{
    public interface IConversationService
    {
        Task<Conversation> CreateAsync(Listing listing);

        Task<Conversation?> GetByIdAsync(int id);

        Task<List<Conversation>> GetAllAsync();

        Task<bool> CheckIfExistsAsync(int userId, int listingId);
    }
}
