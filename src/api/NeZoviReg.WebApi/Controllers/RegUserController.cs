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

namespace NeZoviReg.WebApi.Controllers;

[HasPermission(PermissionType.All)]
public class RegUserController : NeZoviRegBaseController
{
    public RegUserController(ISender sender, ILogger<RegUserController> logger)
    :base(sender, logger)
    {
    }

    [HttpPost("reguser/login")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [AllowAnonymous]
    public async Task<IActionResult> LoginRegUser([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpPost("reguser/register")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RegisterRegUser([FromBody] RegisterRegUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateRegUserCommand(request.Email, request.UserName, request.Password, request.FirstName, request.LastName, request.Roles)
                         .AddAppUser(AppUser);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpPatch("reguser/{regUserId}")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ModifyRegUser(Guid regUserId, [FromBody] ModifyRegUserRequest request, CancellationToken cancellationToken)
    {
        var command = new ModifyRegUserCommand(regUserId, request.Email, request.UserName, request.Password,
                request.FirstName, request.LastName, request.Roles.ToIntList())
            .AddAppUser(AppUser);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpDelete("reguser/{regUserId}")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveRegUser(Guid regUserId, CancellationToken cancellationToken)
    {
        var command = new RemoveRegUserCommand(regUserId)
            .AddAppUser(AppUser);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}