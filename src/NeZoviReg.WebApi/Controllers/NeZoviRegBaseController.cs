using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Domain.Shared;

namespace NeZoviReg.WebApi.Controllers;

[ApiController]
public class NeZoviRegBaseController: ControllerBase
{
    protected readonly ISender Sender;

    protected NeZoviRegBaseController(ISender sender) => Sender = sender;

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