namespace Chat_API.DTOs.Requests.Groups;

public class AddMembersToGroupRequest
{
    public IEnumerable<Guid> MemberIds { get; init; } = null!;
}