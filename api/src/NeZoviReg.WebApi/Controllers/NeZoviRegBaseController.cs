using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Model;
using NeZoviReg.WebApi.Extensions.WebApi;

namespace NeZoviReg.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
[ProducesResponseType(typeof(string), (int)HttpStatusCode.TooManyRequests)]
[ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
public class NeZoviRegBaseController : ControllerBase
{
    protected readonly ISender Sender;
    protected readonly ILogger<NeZoviRegBaseController> Logger;

    protected NeZoviRegBaseController(ISender sender, ILogger<NeZoviRegBaseController> logger)
    {
        Sender = sender;
        Logger = logger;
    }

    protected AppUser AppUser => AppUser.GetUser(User);

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