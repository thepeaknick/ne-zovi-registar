using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.WebApi.Model.User;

namespace NeZoviReg.WebApi.Controllers;

[Route("users")]
[EnableCors("any")]
public class UserController : NeZoviRegBaseController
{
    public UserController(ISender sender)
        : base(sender)
    {
    }

    /// <summary>
    /// Registruj novi telefonski broj.
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
    /// Registruj listu telefonskih brojeva.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("bulkadd")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [HasPermission(PermissionType.Write)]
    public async Task<IActionResult> AddUsers([FromBody] BulkAddUsersRequest request, CancellationToken cancellationToken)
    {
        var command = new AddUsersCommand(request.Users)
            .AddAppUser(AppUser.UserName);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    /// <summary>
    /// Izmeni podatke već registrovanog telefonskog broja.
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
    /// Obriši registrovani telefonski broj.
    /// </summary>
    /// <param name="phoneNumber">Borj telefona koji e brišse iz registra</param>
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
    /// Registrovani telefonski brojevi.
    /// </summary>
    /// <param name="after">Datum od kad nam treba sadrzaj registra</param>
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
    /// Provera da li je telefonski broj registrovan.
    /// </summary>
    /// <param name="phoneNumber">Telefonski broj</param>
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
    
    /// <summary>
    /// Detalji vlasnika registrovanog telefonskog broj.
    /// </summary>
    /// <param name="phoneNumber">Telefonski broj</param>
    /// <returns></returns>
    [HttpGet("{phoneNumber:required}/details")]
    [ProducesResponseType(typeof(UserDetailsDto), (int)HttpStatusCode.OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserDetails(string phoneNumber, CancellationToken cancellationToken)
    {
        var query = new GetUserDetailsQuery(phoneNumber);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}