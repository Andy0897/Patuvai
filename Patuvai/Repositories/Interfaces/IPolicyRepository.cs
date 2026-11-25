using Patuvai.Models;
using Patuvai.Models.Enums;

namespace Patuvai.Repositories.Interfaces
{
    public interface IPolicyRepository
    {
        Task<Policy> GetByIdAsync(int id);

        Task<List<Policy>> GetAllAsync();

        Task<Policy> GetActiveVersionAsync(PolicyType type);

        Task AddAsync(Policy policy);

        Task UpdateAsync(Policy policy);
    }
}
