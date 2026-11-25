using Patuvai.Models;

namespace Patuvai.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> CreateAsync(Message message);
    }
}
