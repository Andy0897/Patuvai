using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patuvai.Models;
using Patuvai.Repositories;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.OfferListing;

namespace Patuvai.Controllers
{
    public class OfferListingController : Controller
    {
        private readonly ICityService _cityService;

        public OfferListingController(ICityService cityService)
        {
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new OfferListingFormViewModel();

            await PopulateCitiesViewDataAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfferListingFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            await PopulateCitiesViewDataAsync();

            return View(model);
        }

        private async Task PopulateCitiesViewDataAsync()
        {
            List<City> cities = await _cityService.GetAllAsync();
            ViewData["Cities"] = cities;
        }
    }
}
