using Patuvai.Models.Enums;

namespace Patuvai.Services.Interfaces
{
    public interface IUserPolicyAcceptanceService
    {
        Task acceptPolicyAsync(int userId, PolicyType policyType);
    }
}
