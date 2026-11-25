using Patuvai.Models;
using Patuvai.Models.Enums;
using Patuvai.ViewModels.Policy;

namespace Patuvai.Services.Interfaces
{
    public interface IPolicyService
    {
        Task AddNewVersionAsync(PolicyFormViewModel model);

        Task<Policy> GetActiveVersionAsync(PolicyType type);
    }
}
