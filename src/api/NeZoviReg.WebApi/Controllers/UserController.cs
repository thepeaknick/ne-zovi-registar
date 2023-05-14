using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.WebApi.Model.User;

namespace NeZoviReg.WebApi.Controllers;

[Route("user")]
[AllowAnonymous]
public class UserController : NeZoviRegBaseController
{
    public UserController(ISender sender,
        ILogger<UserController> logger)
        : base(sender, logger)
    {
    }

    [HttpPost("add")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    //[HasPermission(PermissionType.Write)]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest request, CancellationToken cancellationToken)
    {
        var command = new AddUserCommand(request.FirstName, request.LastName, request.PhoneNumber, request.Jmbg, request.OperatorId)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpPatch("{phoneNumber}")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    //[HasPermission(PermissionType.Write)]
    public async Task<IActionResult> ModifyUser(string phoneNumber, [FromBody] ModifyUserRequest request, CancellationToken cancellationToken)
    {
        var command = new ModifyUserCommand(phoneNumber, request.FirstName, request.LastName, request.Jmbg, request.PhoneNumber, request.OperatorId)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /*[HttpDelete("{phoneNumber}")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.Delete)]
    public async Task<IActionResult> RemoveUser(string phoneNumber, CancellationToken cancellationToken)
    {
        var command = new RemoveUserCommand(phoneNumber)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }*/

    [HttpGet("all")]
    //[HasPermission(PermissionType.ReadAll)]
    [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AllUsers([FromBody] AllUsersRequest request, CancellationToken cancellationToken)
    {
        var command = new AllUsersQuery(request.After);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpGet("{phoneNumber}")]
    //[AllowAnonymous]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUser(string phoneNumber, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(phoneNumber);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value.PhoneNumber);
    }
}