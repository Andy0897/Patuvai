using Patuvai.Data;
using Patuvai.Models;
using Patuvai.Services.Interfaces;

namespace Patuvai.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Message> CreateAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }
    }
}
