using System.Security.Claims;
using Patuvai.Models;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services.Interfaces;

namespace Patuvai.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConversationService(IConversationRepository conversationRepository, IHttpContextAccessor httpContextAccessor)
        {
            _conversationRepository = conversationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Conversation> CreateAsync(Listing listing)
        {
            string currentUserId = _httpContextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User is not logged in.");

            Conversation conversation = new()
            {
                ListingId = listing.Id,
                ListingOwnerId = listing.PublisherId,
                ListingUserId = int.Parse(currentUserId),
                Messages = new List<Message>()
            };
            await _conversationRepository.AddAsync(conversation);
            var conversationId = conversation.Id;
            return conversation;
        }

        public async Task<Conversation?> GetByIdAsync(int id)
        {
            return await _conversationRepository.GetByIdAsync(id);
        }

        public async Task<List<Conversation>> GetAllAsync()
        {
            return await _conversationRepository.GetAllAsync();
        }

        public async Task<bool> CheckIfExistsAsync(int userId, int listingId)
        {
            return await _conversationRepository.CheckIfExistsAsync(userId, listingId);
        }
    }
}
