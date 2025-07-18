namespace Chat_API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";
    private const string AuthenticationBase = $"{ApiBase}/auth";
    private const string UsersBase = $"{ApiBase}/users";
    private const string FriendRequestsBase = $"{ApiBase}/friend-requests";

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
}