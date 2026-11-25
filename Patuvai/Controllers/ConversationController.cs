using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services.Interfaces;
using Patuvai.ViewModels.Conversation;

namespace Patuvai.Controllers
{
    public class ConversationController : Controller
    {
        private readonly IConversationService _conversationService;
        private readonly IListingRepository _listingRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConversationController(IConversationService conversationService, IListingRepository listingRepository, IHttpContextAccessor httpContextAccessor  )
        {
            _conversationService = conversationService;
            _listingRepository = listingRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("/Conversation/Create/{listingId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(int listingId)
        {
            var listing = await _listingRepository.GetByIdAsync(listingId);
            if (listing == null)
            {
                return NotFound();
            }
            int currentUserId = int.Parse(_httpContextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User is not logged in."));

            if (await _conversationService.CheckIfExistsAsync(currentUserId, listingId))
            {
                return BadRequest("Conversation already exists.");
            }

            if(listing.PublisherId == currentUserId)
            {
                return BadRequest("You cannot create a conversation with your own listing.");
            }

            var conversation = await _conversationService.CreateAsync(listing);

            return RedirectToAction("Details", new { id = conversation.Id });
        }

        [HttpGet("/Conversation/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int id)
        {
            var conversation = await _conversationService.GetByIdAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }

            ConversationDetailsViewModel model = new ConversationDetailsViewModel
            {
                ConversationId = conversation.Id,
                Listing = conversation.Listing,
                ListingOwner = conversation.ListingOwner,
                ListingUserId = conversation.ListingUserId,
                ListingUser = conversation.ListingUser,
                Messages = conversation.Messages
            };

            return View(model);
        }
    }
}
