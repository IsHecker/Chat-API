namespace Chat_API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";
    private const string AuthenticationBase = $"{ApiBase}/auth";
    private const string UsersBase = $"{ApiBase}/users";
    private const string FriendRequestsBase = $"{ApiBase}/friend-requests";
    private const string ConversationsBase = $"{ApiBase}/conversations";
    private const string GroupsBase = $"{ApiBase}/groups";

    public static class Auth
    {
        public const string Signin = $"{AuthenticationBase}/signin";
        public const string Register = $"{AuthenticationBase}/register";
    }

    public static class Users
    {
        public const string GetProfile = $"{UsersBase}/me";
        public const string UpdateProfile = $"{UsersBase}/me";
    }

    public static class FriendRequests
    {
        public const string SendRequest = FriendRequestsBase;
        public const string FriendRequestAcceptance = $"{FriendRequestsBase}/{{requestId}}";
    }

    public static class Conversations
    {
        public const string ListAllConversations = ConversationsBase;
        public const string ListConversationMessages = $"{ConversationsBase}/{{conversationId}}/messages";
    }

    public static class Groups
    {
        public const string GetGroupDetails = $"{GroupsBase}/{{conversationId}}";
        public const string ListGroupMembers = $"{GroupsBase}/{{conversationId}}/members";
        public const string AddMembersToGroup = $"{GroupsBase}/{{conversationId}}/members";
        public const string RemoveMemberFromGroup = $"{GroupsBase}/{{conversationId}}/members/{{memberId}}";
        public const string Create = GroupsBase;
        public const string UpdateGroupDetails = $"{GroupsBase}/{{conversationId}}";
    }
}