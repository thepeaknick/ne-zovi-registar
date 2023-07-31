using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Messaging.Auth.Model;
using NeZoviReg.WebApi.Model.Token;
using Microsoft.AspNetCore.RateLimiting;
using NeZoviReg.WebApi.Infrastructure;

namespace NeZoviReg.WebApi.Controllers;

[Route("tokens")]
public class TokenController : NeZoviRegBaseController
{
    public TokenController(ISender sender)
        : base(sender)
    { }

    /// <summary>
    /// Korisnik registra. Resetuj sigurnosni token za pristup registru.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(LoginResultDto), (int)HttpStatusCode.OK)]
    [AllowAnonymous]
    [EnableRateLimiting(Const.AnonymousLogin)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var command = new RefreshTokenCommand(request.AccessToken, request.RefreshToken);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}