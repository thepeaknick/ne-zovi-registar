using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.WebApi.Model.RegUser;
using System.Net;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;
using NeZoviReg.Abstractions.Shared.Model.Auth;

namespace NeZoviReg.WebApi.Controllers;


[Route("regusers")]
[HasPermission(PermissionType.All)]
public class RegUserController : NeZoviRegBaseController
{
    public RegUserController(ISender sender, ILogger<RegUserController> logger)
    : base(sender, logger)
    {
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResult), (int)HttpStatusCode.OK)]
    [AllowAnonymous]
    public async Task<IActionResult> LoginRegUser([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Username, request.Password);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegUserDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RegisterRegUser([FromBody] RegisterRegUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateRegUserCommand(request.Name, request.Address, request.RegNumber, request.TaxNumber, request.FirstName, request.LastName,
                request.UserName, request.Password, request.Roles)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }


    [HttpPatch("modify")]
    [ProducesResponseType(typeof(RegUserDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ModifyRegUser([FromBody] ModifyRegUserRequest request, CancellationToken cancellationToken)
    {
        var command = new ModifyRegUserCommand(AppUser.Id, request.Name, request.Address, request.RegNumber, request.TaxNumber, request.FirstName, request.LastName,
                request.UserName, request.Roles.ToIntList())
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpPatch("{regUserId:required}")]
    [ProducesResponseType(typeof(RegUserDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ModifyRegUser(Guid regUserId, [FromBody] ModifyRegUserRequest request, CancellationToken cancellationToken)
    {
        var command = new ModifyRegUserCommand(regUserId, request.Name, request.Address, request.RegNumber, request.TaxNumber, request.FirstName, request.LastName,
                request.UserName, request.Roles.ToIntList())
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpPatch("forgotpassword")]
    [ProducesResponseType(typeof(RegUserDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ChangeRegUserPassword([FromBody] ChangeRegUserPasswordRequest request, CancellationToken cancellationToken)
    {
        var command = new ChangePassRegUserCommand(AppUser.UserName, request.Password, request.NewPassword)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpDelete("{regUserId:required}")]
    [ProducesResponseType(typeof(RegUserDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveRegUser(Guid regUserId, CancellationToken cancellationToken)
    {
        var command = new RemoveRegUserCommand(regUserId)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpGet("roles/{role:int}")]
    [ProducesResponseType(typeof(List<RegUserDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetRegUsers(RoleType role, CancellationToken cancellationToken)
    {
        var command = new RegUsersQuery(role);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}