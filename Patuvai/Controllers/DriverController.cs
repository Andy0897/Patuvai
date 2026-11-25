using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.Driver;

namespace Patuvai.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriverService _driverService;
        private readonly IReviewService _reviewService;

        public DriverController(IDriverService driverService, IReviewService reviewService)
        {
            _driverService = driverService;
            _reviewService = reviewService;
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var driver = await _driverService.GetByIdAsync(id);

            if (driver == null)
                return NotFound();

            int? currentUserId = null;

            if (User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdClaim, out int userId))
                {
                    currentUserId = userId;
                }
            }

            bool canAddReview = false;

            if (currentUserId.HasValue)
            {
                bool isDriverAccount = driver.UserId == currentUserId.Value;

                bool alreadyReviewed = await _reviewService.IsReviewedByUserAsync(currentUserId.Value, driver.Id);

                canAddReview = !isDriverAccount && !alreadyReviewed;
            }

            DriverDetailsViewModel model = new()
            {
                DriverId = driver.Id,
                UserId = driver.UserId,
                FirstName = driver.User.FirstName,
                LastName = driver.User.LastName,
                Introduction = driver.Introduction,
                AverageRating = driver.AverageRating,
                ReviewsCount = driver.ReviewsCount,
                CreatedAt = driver.CreatedAt,
                CanAddReview = canAddReview
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DriverFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _driverService.CreateAsync(model);

                TempData["Success"] = "Информацията за шофьора беше запазена успешно.";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
