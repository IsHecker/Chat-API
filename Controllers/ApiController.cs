using System.IdentityModel.Tokens.Jwt;
using Chat_API.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Chat_API.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected Guid UserId
    {
        get
        {
            var id = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (id is null || !Guid.TryParse(id.Value, out Guid userId) || userId == Guid.Empty)
                throw new InvalidOperationException("User ID not found or is empty.");

            SharedData.UserId = userId;
            return userId;
        }
    }

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        return errors.All(error => error.Type == ErrorType.Validation) ?
            ValidationProblem(errors)
            : Problem(errors[0]);
    }

    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, detail: error.Description);
    }

    protected IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}