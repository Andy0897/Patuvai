using System.Security.Claims;
using Patuvai.Models;
using Patuvai.Models.Enums;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.RequestListing;

namespace Patuvai.Services
{
    public class RequestListingService : IRequestListingService
    {
        private readonly IRequestListingRepository _requestListingRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestListingService(IRequestListingRepository requestListingRepository, IHttpContextAccessor httpContextAccessor)
        {
            _requestListingRepository = requestListingRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(RequestListingFormViewModel model)
        {
            string currentUserId = _httpContextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User is not logged in.");

            RequestListing requestListing = new()
            {
                TransportServiceType = model.TransportServiceType,
                Description = model.Description,
                Budget = model.Budget,
                FromCityId = model.FromCityId,
                ToCityId = model.ToCityId,
                PublisherId = int.Parse(currentUserId),
                CreatedAt = DateTime.UtcNow,
                Status = RequestListingStatus.AVAILABLE
            };
            await _requestListingRepository.AddAsync(requestListing);
        }

        public async Task UpdateAsync(RequestListingEditFormViewModel model)
        {
            RequestListing? requestListing = await _requestListingRepository.GetByIdAsync(model.Id);
            if (requestListing == null)
            {
                throw new KeyNotFoundException($"Request listing with ID {model.Id} not found.");
            }
            string currentUserId = _httpContextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User is not logged in.");
            if (requestListing.PublisherId != int.Parse(currentUserId))
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this request listing.");
            }
            requestListing.RequestDate = model.RequestDate;
            requestListing.TransportServiceType = model.TransportServiceType;
            requestListing.Description = model.Description;
            requestListing.Budget = model.Budget;
            await _requestListingRepository.UpdateAsync(requestListing);
        }

        public async Task<List<RequestListing>> GetAllAsync()
        {
            return await _requestListingRepository.GetAllAsync();
        }

        public async Task<RequestListing?> GetByIdAsync(int id)
        {
            return await _requestListingRepository.GetByIdAsync(id);
        }
    }
}
