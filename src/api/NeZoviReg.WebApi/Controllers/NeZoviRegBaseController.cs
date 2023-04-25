using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Auth.Authentication.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = NeZoviReg.Abstractions.Shared.Error;

namespace NeZoviReg.WebApi.Controllers;

[ApiController]
public class NeZoviRegBaseController : ControllerBase
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
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(CreateProblemDetails("Validaciona greška",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.ErrorsDictionary)),
            _ =>
                BadRequest(
                    CreateProblemDetails(
                        "Loš zahtev",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        errors: null))
        };

    /*private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };*/

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Dictionary<string, string[]>? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}