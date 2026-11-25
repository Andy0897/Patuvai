using Patuvai.Models;
using Patuvai.Models.Enums;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.Policy;

namespace Patuvai.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;

        public PolicyService(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task AddNewVersionAsync(PolicyFormViewModel model)
        {
            Policy lastPolicy = await GetActiveVersionAsync(model.Type);

            Policy policy = new Policy
            {
                Type = model.Type,
                Content = model.Content,
                CreatedAt = DateTime.Now,
                Version = lastPolicy != null && lastPolicy.Version != 0 ? lastPolicy.Version + 1 : 1.0,
                IsActive = true
            };

            if (lastPolicy != null)
            {
                lastPolicy.IsActive = false;
                await _policyRepository.UpdateAsync(lastPolicy);
            }

            await _policyRepository.AddAsync(policy);
        }

        public async Task<Policy> GetActiveVersionAsync(PolicyType type)
        {
            return await _policyRepository.GetActiveVersionAsync(type);
        }
    }
}
