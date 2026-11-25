using Patuvai.Models;
using Patuvai.Models.Enums;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services.Interfaces;

namespace Patuvai.Services
{
    public class UserPolicyAcceptanceService : IUserPolicyAcceptanceService
    {
        private readonly IUserPolicyAcceptanceRepository _userAcceptanceRepository;
        private readonly IPolicyRepository _policyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserPolicyAcceptanceService(IUserPolicyAcceptanceRepository userPolicyAcceptanceRepository, IPolicyRepository policyRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userAcceptanceRepository = userPolicyAcceptanceRepository;
            _policyRepository = policyRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task acceptPolicyAsync(int userId, PolicyType policyType)
        {
            // Resolve HTTP context safely; it may be null when called outside an active request
            var context = _httpContextAccessor?.HttpContext;

            // Await the async repository call instead of blocking with .Result
            var policy = await _policyRepository.GetActiveVersionAsync(policyType);

            // If there is no active policy for the requested type, do not throw NRE — bail out or handle as needed
            if (policy == null)
            {
                // Option: log a warning here. For now, simply return to avoid NullReferenceException.
                return;
            }

            int policyId = policy.Id;

            UserPolicyAcceptance userPolicyAcceptance = new UserPolicyAcceptance
            {
                UserId = userId,
                PolicyId = policyId,
                AcceptedAt = DateTime.UtcNow,
                IpAddress = context?.Connection?.RemoteIpAddress?.ToString(),
                UserAgent = context?.Request?.Headers["User-Agent"].ToString()
            };

            await _userAcceptanceRepository.AddAsync(userPolicyAcceptance);
        }
    }
}
