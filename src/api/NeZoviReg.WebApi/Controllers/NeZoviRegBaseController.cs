using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Model;
using NeZoviReg.Auth.Authentication.Services;
using NeZoviReg.WebApi.Extensions.WebApi;

namespace NeZoviReg.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
[ProducesResponseType(typeof(string), (int)HttpStatusCode.TooManyRequests)]
[ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
public class NeZoviRegBaseController : ControllerBase
{
    protected readonly ISender Sender;
    protected readonly ILogger<NeZoviRegBaseController> Logger;

    protected NeZoviRegBaseController(ISender sender, ILogger<NeZoviRegBaseController> logger)
    {
        Sender = sender;
        Logger = logger;
    }

    protected AppUser AppUser => new (Guid.Parse(User.Claims
        .FirstOrDefault(x => x.Type == CustomClaims.RegUserId)?
        .Value ?? string.Empty),
         User.Claims
        .FirstOrDefault(x => x.Type == CustomClaims.RegUserName)?
        .Value ?? string.Empty);

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(WebApiExtensions.CreateProblemDetails("Greška u validaciji",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.ErrorsDictionary)),
            _ =>
                BadRequest(
                    WebApiExtensions.CreateProblemDetails(
                        "Loš zahtev",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        errors: null))
        };
}