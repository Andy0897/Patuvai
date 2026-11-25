using Patuvai.Models;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services.Interfaces;

namespace Patuvai.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task<List<City>> GetAllAsync()
        {
            return await _cityRepository.GetAllAsync();
        }
        public async Task<City?> GetByIdAsync(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }
    }
}
