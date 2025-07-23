using Chat_API.DTOs.Requests.Common;
using Chat_API.Models;

namespace Chat_API.Data.Repositories;

public class MessageRepository : EntityRepository<Message, MessageRepository>
{
    public MessageRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<IEnumerable<Message>> ListConversationMessagesAsync(Guid conversationId, Pagination pagination)
    {
        return Task.FromResult<IEnumerable<Message>>(
            Query.Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.SentAt)
                .Paginate(pagination));
    }
}