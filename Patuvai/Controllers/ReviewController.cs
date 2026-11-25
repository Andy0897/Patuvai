using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.Review;

namespace Patuvai.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        [Route("Review/Create/{driverId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(int driverId)
        {
            ReviewFormViewModel model = new()
            {
                DriverId = driverId,
                Rating = 0,
                Comment = string.Empty
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _reviewService.CreateAsync(model);
            return RedirectToAction("Details", "Driver", new { id = model.DriverId });
        }
    }
}
