using Patuvai.Models;

namespace Patuvai.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message?> GetByIdAsync(int id);

        Task<List<Message>> GetAllAsync();

        Task AddAsync(Message message);

        Task UpdateAsync(Message message);

        Task DeleteAsync(int id);
    }
}
