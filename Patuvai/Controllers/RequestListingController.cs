using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patuvai.Models;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.RequestListing;

namespace Patuvai.Controllers
{
    public class RequestListingController : Controller
    {
        private readonly IRequestListingService _requestListingService;
        private readonly ICityService _cityService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestListingController(IRequestListingService requestListingService, ICityService cityService, IHttpContextAccessor httpContextAccessor)
        {
            _requestListingService = requestListingService;
            _cityService = cityService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create()
        {
            var model = new RequestListingFormViewModel();

            await PopulateCitiesViewDataAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(RequestListingFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCitiesViewDataAsync();
                return View(model);
            }

            try
            {
                await _requestListingService.CreateAsync(model);

                TempData["Success"] = "Информацията за обявата беше запазена успешно.";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                await PopulateCitiesViewDataAsync();
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id)
        {
            RequestListing? requestListing = await _requestListingService.GetByIdAsync(id);
            if (requestListing == null)
            {
                return NotFound();
            }
            int currentUserId = int.Parse(_httpContextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User is not logged in."));
            if (requestListing.PublisherId != currentUserId)
            {
                return Forbid();
            }
            var model = new RequestListingEditFormViewModel
            {
                Id = requestListing.Id,
                RequestDate = requestListing.RequestDate,
                TransportServiceType = requestListing.TransportServiceType,
                Description = requestListing.Description,
                Budget = requestListing.Budget
            };

            await PopulateCitiesViewDataAsync();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(RequestListingEditFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCitiesViewDataAsync();
                return View(model);
            }
            try
            {
                await _requestListingService.UpdateAsync(model);
                TempData["Success"] = "Информацията за обявата беше обновена успешно.";
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                await PopulateCitiesViewDataAsync();
                return View(model);
            }
        }

        public async Task<IActionResult> List()
        {
            List<RequestListing> requestListings = await _requestListingService.GetAllAsync();
            return View(requestListings);
        }

        public async Task<IActionResult> Details(int id)
        {
            RequestListing? requestListing = await _requestListingService.GetByIdAsync(id);

            if (requestListing == null)
            {
                return NotFound();
            }

            int currentUserId = int.Parse(_httpContextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User is not logged in."));

            RequestListingDetailsViewModel model = new()
            {
                Id = requestListing.Id,
                RequestDate = requestListing.RequestDate,
                TransportServiceType = requestListing.TransportServiceType,
                Description = requestListing.Description,
                Budget = requestListing.Budget,
                FromCity = requestListing.FromCity,
                ToCity = requestListing.ToCity,
                Publisher = requestListing.Publisher,
                CreatedAt = requestListing.CreatedAt,
                isOwner = requestListing.PublisherId == currentUserId,
            };

            return View(model);
        }
        private async Task PopulateCitiesViewDataAsync()
        {
            List<City> cities = await _cityService.GetAllAsync();
            ViewData["Cities"] = cities;
        }
    }
}