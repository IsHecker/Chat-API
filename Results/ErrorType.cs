namespace Chat_API.Results;

public enum ErrorType
{
    None,
    NotFound,
    Validation,
    Unauthorized,
    Conflict,
    Unexpected,
    Forbidden,
    Failure
}