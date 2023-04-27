using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.WebApi.Model.User;

namespace NeZoviReg.WebApi.Controllers;

[HasPermission(PermissionType.All)]
public class UserController : NeZoviRegBaseController
{
    public UserController(ISender sender,
        ILogger<UserController> logger)
        : base(sender, logger)
    {
    }

    [HttpPost("user/add")]
    [HasPermission(PermissionType.Write)]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest request, CancellationToken cancellationToken)
    {
        var command = new AddUserCommand(request.FirstName, request.LastName, request.Jmbg, request.PhoneNumber)
            .AddAppUser(AppUser);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpDelete("user/{phoneNumber}")]
    [HasPermission(PermissionType.Delete)]
    public async Task<IActionResult> RemoveUser(string phoneNumber, CancellationToken cancellationToken)
    {
        var command = new RemoveUserCommand(phoneNumber)
            .AddAppUser(AppUser);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpGet("user/all")]
    [HasPermission(PermissionType.ReadAll)]
    public async Task<IActionResult> AllUsers(CancellationToken cancellationToken)
    {
        var command = new AllUsersCommand()
            .AddAppUser(AppUser);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpGet("user/{phoneNumber}")]
    [HasPermission(PermissionType.Read)]
    public async Task<IActionResult> GetUser(string phoneNumber, CancellationToken cancellationToken)
    {
        var command = new GetUserCommand(phoneNumber)
            .AddAppUser(AppUser);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}