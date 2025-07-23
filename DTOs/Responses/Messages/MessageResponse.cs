namespace Chat_API.DTOs.Responses.Messages;

public class MessageResponse
{
    public Guid SenderId { get; init; }
    public string Content { get; init; } = null!;
    public string Status { get; init; } = null!;
    public DateTime SentAt { get; init; }
}