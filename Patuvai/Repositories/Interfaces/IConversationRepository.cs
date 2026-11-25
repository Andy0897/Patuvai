using Patuvai.Models;

namespace Patuvai.Repositories.Interfaces
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetByIdAsync(int id);

        Task<List<Conversation>> GetAllAsync();

        Task AddAsync(Conversation conversation);

        Task UpdateAsync(Conversation conversation);

        Task DeleteAsync(int id);

        Task<bool> CheckIfExistsAsync(int userId, int listingId);
    }
}
