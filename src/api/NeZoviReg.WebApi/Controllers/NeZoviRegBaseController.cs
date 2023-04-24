using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Auth.Authentication.Services;

namespace NeZoviReg.WebApi.Controllers;

[ApiController]
public class NeZoviRegBaseController: ControllerBase
{
    protected readonly ISender Sender;
    protected readonly ILogger<NeZoviRegBaseController> Logger;

    protected NeZoviRegBaseController(ISender sender, ILogger<NeZoviRegBaseController> logger)
    {
        Sender = sender;
        Logger = logger;
    }

    protected string AppUser => User.Claims
                               .FirstOrDefault(x => x.Type == CustomClaims.RegUserName)?
                               .Value ?? string.Empty;

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            {IsSuccess : true} => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(CreateProblemDetails("Validaciona greška",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.Errors)),
            _ =>
                BadRequest(
                    CreateProblemDetails(
                        "Loš zahtev",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code.ToString(),
            Detail = error.Message,
            Status = status,
            Extensions = {{nameof(errors), errors}}
        };
}