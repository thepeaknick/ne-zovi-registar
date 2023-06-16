using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.WebApi.Model.RegUser;
using System.Net;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;
using Serilog;

namespace NeZoviReg.WebApi.Controllers;

[Route("regusers")]
public class RegUserController : NeZoviRegBaseController
{
    public RegUserController(ISender sender)
        : base(sender)
    { }

    /// <summary>
    /// Registruj novog korisnika registra.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegUserDto), (int) HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly)]
    public async Task<IActionResult> RegisterRegUser([FromBody] RegisterRegUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateRegUserCommand(request.Name, request.Email, request.Address, request.RegNumber,
                request.TaxNumber, request.FirstName, request.LastName,
                request.UserName, request.Password, request.Role)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Izmeni ulogovanog korisnika registra.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("modify")]
    [ProducesResponseType(typeof(RegUserDto), (int) HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly | PermissionType.Read)]
    public async Task<IActionResult> ModifyRegUser([FromBody] ModifyRegUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ModifyRegUserCommand(AppUser.Id, request.Name, request.Email, request.Address,
                request.RegNumber, request.TaxNumber, request.FirstName, request.LastName,
                request.UserName, (int?)request.Role)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Izmeni postojećeg korisnika registra.
    /// </summary>
    /// <param name="regUserId"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("{regUserId:required}")]
    [ProducesResponseType(typeof(RegUserDto), (int) HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly)]
    public async Task<IActionResult> ModifyRegUser(Guid regUserId, [FromBody] ModifyRegUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ModifyRegUserCommand(regUserId, request.Name, request.Email, request.Address,
                request.RegNumber, request.TaxNumber, request.FirstName, request.LastName,
                request.UserName, (int?)request.Role)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Obriši postojećeg korisnika registra.
    /// </summary>
    /// <param name="regUserId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{regUserId:required}")]
    [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly)]
    public async Task<IActionResult> RemoveRegUser(Guid regUserId, CancellationToken cancellationToken)
    {
        var command = new RemoveRegUserCommand(regUserId)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Detalji registrovanog korisnika registra.
    /// </summary>
    /// <param name="regUserId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{regUserId:required}")]
    [ProducesResponseType(typeof(RegUserDetailsDto), (int) HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly | PermissionType.Read)]
    public async Task<IActionResult> GetRegUser(Guid regUserId, CancellationToken cancellationToken)
    {
        var command = new RegUserQuery(regUserId);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Registrovane role korisnika registra.
    /// </summary>
    /// <param name="role"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("roles/{role:int}")]
    [ProducesResponseType(typeof(List<RegUserDto>), (int) HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly)]
    public async Task<IActionResult> GetRegUsers(int role, CancellationToken cancellationToken)
    {
        var command = new RegUsersQuery((RoleType) role);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Korisnik registra. Pošalji mejl.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("email")]
    [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly | PermissionType.Read)]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request, CancellationToken cancellationToken)
    {
        var command = new SendEmailCommand(request.FirstName, request.LastName, request.CompanyName, request.EmailFrom,
                request.PhoneNumber, request.Content)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}