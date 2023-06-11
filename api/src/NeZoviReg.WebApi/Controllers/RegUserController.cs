using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.WebApi.Model.RegUser;
using System.Net;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;

namespace NeZoviReg.WebApi.Controllers;


[Route("regusers")]
public class RegUserController : NeZoviRegBaseController
{
    public RegUserController(ISender sender, ILogger<RegUserController> logger)
    : base(sender, logger)
    {
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegUserDto), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly)]
    public async Task<IActionResult> RegisterRegUser([FromBody] RegisterRegUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateRegUserCommand(request.Name, request.Email, request.Address, request.RegNumber, request.TaxNumber, request.FirstName, request.LastName,
                request.UserName, request.Password, request.Roles)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }


    [HttpPatch("modify")]
    [ProducesResponseType(typeof(RegUserDto), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly | PermissionType.Read)]
    public async Task<IActionResult> ModifyRegUser([FromBody] ModifyRegUserRequest request, CancellationToken cancellationToken)
    {
        var command = new ModifyRegUserCommand(AppUser.Id, request.Name, request.Email, request.Address, request.RegNumber, request.TaxNumber, request.FirstName, request.LastName,
                request.UserName, request.Roles.ToIntList())
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpPatch("{regUserId:required}")]
    [ProducesResponseType(typeof(RegUserDto), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly)]
    public async Task<IActionResult> ModifyRegUser(Guid regUserId, [FromBody] ModifyRegUserRequest request, CancellationToken cancellationToken)
    {
        var command = new ModifyRegUserCommand(regUserId, request.Name, request.Email, request.Address, request.RegNumber, request.TaxNumber, request.FirstName, request.LastName,
                request.UserName, request.Roles.ToIntList())
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpDelete("{regUserId:required}")]
    [ProducesResponseType(typeof(RegUserDto), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly)]
    public async Task<IActionResult> RemoveRegUser(Guid regUserId, CancellationToken cancellationToken)
    {
        var command = new RemoveRegUserCommand(regUserId)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpGet("{regUserId:required}")]
    [ProducesResponseType(typeof(RegUserDetailsDto), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly | PermissionType.Read)]
    public async Task<IActionResult> GetRegUser(Guid regUserId, CancellationToken cancellationToken)
    {
        var command = new RegUserQuery(regUserId);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpGet("roles/{role:int}")]
    [ProducesResponseType(typeof(List<RegUserDto>), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly)]
    public async Task<IActionResult> GetRegUsers(int role, CancellationToken cancellationToken)
    {
        var command = new RegUsersQuery((RoleType)role);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpPost("email")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly | PermissionType.Read)]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request, CancellationToken cancellationToken)
    {
        var command = new SendEmailCommand(request.FirstName, request.LastName, request.CompanyName, request.EmailFrom, request.PhoneNumber, request.Content)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

}