using Patuvai.Models;

namespace Patuvai.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<City?> GetByIdAsync(int id);

        Task<List<City>> GetAllAsync();

        Task AddAsync(City city);

        Task UpdateAsync(City city);

        Task DeleteAsync(int id);
    }
}
