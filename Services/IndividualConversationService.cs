using Chat_API.Data;
using Chat_API.Data.Repositories;
using Chat_API.DTOs.Requests.Common;
using Chat_API.DTOs.Responses.Conversations;
using Chat_API.DTOs.Responses.Users;
using Chat_API.Mappers;

namespace Chat_API.Services;

public class IndividualConversationService
{
    private readonly IndividualConversationRepository _individualRepository;
    private readonly ApplicationDbContext _dbContext;

    public IndividualConversationService(IndividualConversationRepository individualRepository, ApplicationDbContext dbContext)
    {
        _individualRepository = individualRepository;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ConversationResponse>> GetAllByUserIdAsync(Guid userId, Pagination pagination)
    {
        var individualConversations = await _individualRepository.GetAllByUserIdAsync(userId, pagination);

        var friends = _dbContext.Users
                .Where(u => individualConversations
                    .Any(conv => u.Id == (conv.User1Id == userId ? conv.User2Id : conv.User1Id)))
                .Select(friend => new UserResponse
                {
                    Id = friend.Id,
                    Username = friend.UserName!,
                    ProfilePictureUrl = friend.ProfilePictureUrl
                })
               .ToList();

        // specific mapping logic.
        return individualConversations.Select(conv =>
        {
            var response = conv.ToResponse();
            response.Friend = friends.First(f => f.Id == conv.FriendId);
            return response;
        });
    }
}