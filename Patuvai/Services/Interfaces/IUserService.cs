using Patuvai.Models;
using Patuvai.ViewModels.Account;

namespace Patuvai.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterViewModel model);

        Task<ApplicationUser?> LoginAsync(LoginViewModel model);

        Task<List<ApplicationUser>> GetAllAsync();

        Task<ApplicationUser?> GetByIdAsync(int id);
    }
}
