using System.Security.Claims;
using Patuvai.Models;
using Patuvai.Models.Enums;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.Driver;

namespace Patuvai.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserPolicyAcceptanceService _userPolicyAcceptanceService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DriverService(IDriverRepository driverRepository,
            IUserRepository userRepository,
            IUserPolicyAcceptanceService userPolicyAcceptanceService,
            IHttpContextAccessor httpContextAccessor)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _userPolicyAcceptanceService = userPolicyAcceptanceService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(DriverFormViewModel model)
        {
            string currentUserId = _httpContextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User is not logged in.");

            Driver driver = new()
            {
                UserId = int.Parse(currentUserId),
                Introduction = model.Introduction,
                AverageRating = 0.0,
                ReviewsCount = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            await _driverRepository.AddAsync(driver);
            await _userPolicyAcceptanceService.acceptPolicyAsync(driver.UserId, PolicyType.DriverDeclaration);
        }

        public async Task<DriverDetailsViewModel?> GetDriverDetailsAsync(int driverId)
        {
            var driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null)
            {
                return null;
            }
            ApplicationUser user = await _userRepository.GetByIdAsync(driver.UserId);
            return new DriverDetailsViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Introduction = driver.Introduction,
                AverageRating = driver.AverageRating,
                ReviewsCount = driver.ReviewsCount,
                CreatedAt = driver.CreatedAt
            };
        }

        public async Task AddReviewAsync(Review review)
        {
            Driver driver = await _driverRepository.GetByIdAsync(review.DriverId);
            driver.Reviews.Add(review);
            driver.ReviewsCount++;
            driver.AverageRating = await CalculateAverageRatingAsync(driver);


            _driverRepository.UpdateAsync(driver);
        }

        public async Task<List<Driver>> GetAllAsync()
        {
            return await _driverRepository.GetAllAsync();
        }

        public async Task<Driver?> GetByIdAsync(int id)
        {
            return await _driverRepository.GetByIdAsync(id);
        }

        private async Task<double> CalculateAverageRatingAsync(Driver driver)
        {
            if (driver == null || driver.ReviewsCount == 0)
            {
                return 0.0;
            }
            double totalRating = driver.Reviews.Sum(r => r.Rating);
            return totalRating / driver.Reviews.Count;
        }
    }
}
