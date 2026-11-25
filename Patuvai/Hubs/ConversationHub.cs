using Microsoft.AspNetCore.SignalR;
using Patuvai.Models;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services.Interfaces;

namespace Patuvai.Hubs
{
    public interface IConversationHub
    {
        Task ReceiveMessage(Message message);
    }

    public class ConversationHub : Hub<IConversationHub>
    {
        private readonly IMessageService _messageService;
        private readonly IUserRepository _userRepository;

        public ConversationHub(IMessageService messageService, IUserRepository userRepository)
        {
            _messageService = messageService;
            _userRepository = userRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var conversationId = Context.GetHttpContext()?.Request.Query["conversationId"];

            if (!string.IsNullOrEmpty(conversationId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(conversationId!));
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var conversationId = Context.GetHttpContext()?.Request.Query["conversationId"];

            if (!string.IsNullOrEmpty(conversationId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(conversationId!));
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string content, int conversationId)
        {
            var senderId = int.Parse(Context.UserIdentifier!);

            Message message = new Message
            {
                ConversationId = conversationId,
                SenderId = senderId,
                Content = content,
                SentAt = DateTime.UtcNow
            };

            await _messageService.CreateAsync(message);

            message.Sender = await _userRepository.GetByIdAsync(senderId) ?? throw new InvalidOperationException("Sender not found");

            await Clients.Group(GetGroupName(conversationId.ToString())).ReceiveMessage(message);
        }

        private static string GetGroupName(string conversationId) => $"conversation-{conversationId}";
    }
}
