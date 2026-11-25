using Patuvai.Models;

namespace Patuvai.Repositories.Interfaces
{
    public interface IUserPolicyAcceptanceRepository
    {
        Task<UserPolicyAcceptance> GetByIdAsync(int id);

        Task<List<UserPolicyAcceptance>> GetAllAsync();

        Task AddAsync(UserPolicyAcceptance userPolicyAcceptance);
    }
}
