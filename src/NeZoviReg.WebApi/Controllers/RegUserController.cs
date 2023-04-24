using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Application.Services.RegUser;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.Auth.Model.Enum;
using NeZoviReg.Auth.Services.Login;
using NeZoviReg.WebApi.Model.Account;

namespace NeZoviReg.WebApi.Controllers;

public class RegUserController : NeZoviRegBaseController
{
    public RegUserController(ISender sender, ILogger<RegUserController> logger)
    :base(sender, logger)
    {
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpPost("register")]
    [HasPermission(PermissionType.All)]
    public async Task<IActionResult> RegisterRegUser([FromBody] RegisterRegUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateRegUserCommand(request.Email, request.UserName, request.Password, request.FirstName, request.LastName)
                         .AddAppUser(AppUser);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}