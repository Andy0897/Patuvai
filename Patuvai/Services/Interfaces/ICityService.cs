using Patuvai.Models;

namespace Patuvai.Services.Interfaces
{
    public interface ICityService
    {
        Task<List<City>> GetAllAsync();

        Task<City?> GetByIdAsync(int id);
    }
}
