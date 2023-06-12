using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.WebApi.Model.User;

namespace NeZoviReg.WebApi.Controllers;

[Route("users")]
public class UserController : NeZoviRegBaseController
{
    public UserController(ISender sender,
        ILogger<UserController> logger)
        : base(sender, logger)
    {
    }

    /// <summary>
    /// Registruj novog potrošača.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("add")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.Write)]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest request, CancellationToken cancellationToken)
    {
        var command = new AddUserCommand(request.FirstName, request.LastName, request.PhoneNumbers, request.Jmbg, request.OperatorId)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Izmeni registrovanog potrošača.
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("{phoneNumber}")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.Write)]
    public async Task<IActionResult> ModifyUser(string phoneNumber, [FromBody] ModifyUserRequest request, CancellationToken cancellationToken)
    {
        var command = new ModifyUserCommand(phoneNumber, request.FirstName, request.LastName, request.Jmbg, request.PhoneNumber, request.OperatorId)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Obriši registrovanog potrošača.
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{phoneNumber}")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.Delete)]
    public async Task<IActionResult> RemoveUser(string phoneNumber, CancellationToken cancellationToken)
    {
        var command = new RemoveUserCommand(phoneNumber)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    /// <summary>
    /// Registar svih potrošača.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("all/{after:datetime?}")]
    [ProducesResponseType(typeof(List<UserDto>), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.RegUsersOnly | PermissionType.Read)]
    public async Task<IActionResult> AllUsers(DateTime? after, CancellationToken cancellationToken)
    {
        var command = new AllUsersQuery(after);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Proveri da li broj telefona u registru.
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{phoneNumber:required}")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetUser(string phoneNumber, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(phoneNumber);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value.PhoneNumber);
    }
}