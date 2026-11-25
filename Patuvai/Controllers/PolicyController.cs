using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patuvai.Models;
using Patuvai.Models.Enums;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.Policy;

namespace Patuvai.Controllers
{
    public class PolicyController : Controller
    {
        IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet("/Policy/{policyType}")]
        public IActionResult Policy(PolicyType policyType)
        {
            Policy model = _policyService.GetActiveVersionAsync(policyType).Result;
            return View(model);
        }

        [HttpGet("{policyType}/AddVersion")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddVersion(PolicyType policyType)
        {
            Policy lastVersion = await _policyService.GetActiveVersionAsync(policyType);
            PolicyFormViewModel model = new PolicyFormViewModel
            {
                Type = policyType,
                Version = lastVersion != null && lastVersion.Version != 0 ? lastVersion.Version + 0.1 : 1.0
            };
            return View(model);
        }

        [HttpPost("{policyType}/AddVersion")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVersion(PolicyFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                await _policyService.AddNewVersionAsync(model);
                TempData["Success"] = "Нова версия на политиката беше добавена успешно.";
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
