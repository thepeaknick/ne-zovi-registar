using System.Net;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Extensions.Paging;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Enums;
using NeZoviReg.Abstractions.Shared.Model;
using NeZoviReg.WebApi.Model.Paging;
using static NeZoviReg.WebApi.Extensions.WebApi.WebApiExtensions;

namespace NeZoviReg.WebApi.Controllers;

[EnableCors("any")]
[ApiController]
[ApiVersion("1.0")]
[ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
[ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.TooManyRequests)]
[ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
[ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
public class NeZoviRegBaseController : ControllerBase
{
    protected readonly ISender Sender;
    
    protected NeZoviRegBaseController(ISender sender)
    {
        Sender = sender;
    }

    protected AppUser AppUser => AppUser.GetUser(User);

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(CreateProblemDetails("Greška u validaciji",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.ErrorsDictionary)),
            { Error: var e } when e.Code == ErrorCode.NotFound.ToString() => NotFound(CreateProblemDetails("Nema rezultata",
                    StatusCodes.Status404NotFound,
                    result.Error)),
            _ =>
                BadRequest(CreateProblemDetails("Loš zahtev",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };
    
    
    protected PageInfo GetPageInfo()
    {
        return new PageInfo
        {
            CurrentCursor = GetFormOrQueryParameter<long?>(PagingParameterName.Cursor) ?? 1,
            PageSize = GetFormOrQueryParameter<int?>(PagingParameterName.PageSize) ?? PageInfo.DefaultPageSize
        };
    }

    private T GetFormOrQueryParameter<T>(string name)
    {
        string value = null;
        if (Request.HasFormContentType)
            value = Request.Form.GetFirstOrDefault(name);

        if (value == null)
            value = Request.Query.GetFirstOrDefault(name);

        if (value == null)
            return default(T);

        // handle possible nullable structs
        var type = typeof(T);
        var underlyingType = Nullable.GetUnderlyingType(type);

        return (T)Convert.ChangeType(value, underlyingType ?? type);
    }
}