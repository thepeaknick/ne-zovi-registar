using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Messaging.Auth.Model;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.WebApi.Model.RegUser;
using Microsoft.AspNetCore.RateLimiting;
using NeZoviReg.WebApi.Infrastructure;

namespace NeZoviReg.WebApi.Controllers;

[Route("account")]
public class AccountController : NeZoviRegBaseController
{
    public AccountController(ISender sender)
        : base(sender)
    {
    }

    /// <summary>
    /// Korisnik registra. Uloguj se.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResultDto), (int) HttpStatusCode.OK)]
    [AllowAnonymous]
    [EnableRateLimiting(Const.AnonymousLogin)]
    public async Task<IActionResult> LoginRegUser([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Username, request.Password);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Korisnik registra. Izloguj se.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("logout")]
    [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly | PermissionType.Read)]
    public async Task<IActionResult> LogoutRegUser(CancellationToken cancellationToken)
    {
        var command = new LogoutCommand(AppUser.RegUserAccountId);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Korisnik registra. Promeni lozinku.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("resetpassword")]
    [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly | PermissionType.Read)]
    public async Task<IActionResult> ChangeRegUserPassword([FromBody] ChangeRegUserPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangePassCommand(AppUser.UserName, request.Password, request.NewPassword)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Zaboravo si lozinku?
    /// Korisnik registra. Pošalji link na svoj mejl.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("forgotpassword/{email:required}")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [AllowAnonymous]
    [EnableRateLimiting(Const.AnonymousLogin)]
    public async Task<IActionResult> ForgotRegUserPassword(string email, CancellationToken cancellationToken)
    {
        var command = new ForgotPassCommand(email)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Korisnik registra. Resetuj zaboravljenu lozinku.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("forgotpassword")]
    [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
    [AllowAnonymous]
    [EnableRateLimiting(Const.AnonymousLogin)]
    public async Task<IActionResult> ResetRegUserPassword([FromBody] ResetRegUserPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ResetPassCommand(request.Email, request.Token, request.Password)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}