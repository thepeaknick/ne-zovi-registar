using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.WebApi.Model.User;

namespace NeZoviReg.WebApi.Controllers;

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
    public async Task<IActionResult> AllUsers([FromBody] AllUsersRequest request, CancellationToken cancellationToken)
    {
        var command = new AllUsersQuery(request.StartingFrom);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpGet("user/{phoneNumber}")]
    [HasPermission(PermissionType.Read)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUser(string phoneNumber, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(phoneNumber);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value.PhoneNumber);
    }
}