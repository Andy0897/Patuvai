using Patuvai.Models;
using Patuvai.Models.Enums;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.Account;

namespace Patuvai.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordService _passwordService;
        private readonly IUserPolicyAcceptanceService _userPolicyAcceptanceService;

        public UserService(IUserRepository userRepository, PasswordService passwordService, IUserPolicyAcceptanceService userPolicyAcceptanceService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _userPolicyAcceptanceService = userPolicyAcceptanceService;
        }

        public async Task RegisterAsync(RegisterViewModel model)
        {
            if (await _userRepository.EmailExistsAsync(model.Email))
                throw new Exception("Потребител с този имейл вече съществува.");

            ApplicationUser user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = _passwordService.Hash(model.Password),
                Role = Models.Enums.UserRole.User
            };

            await _userRepository.AddAsync(user);
            await _userPolicyAcceptanceService.acceptPolicyAsync(user.Id, PolicyType.Terms);
            await _userPolicyAcceptanceService.acceptPolicyAsync(user.Id, PolicyType.Privacy);
        }

        public async Task<ApplicationUser?> LoginAsync(LoginViewModel model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);

            if (user == null)
                return null;

            bool valid = _passwordService.Verify(
                user.PasswordHash,
                model.Password);

            if (!valid)
                return null;

            return user;
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
    }
}
