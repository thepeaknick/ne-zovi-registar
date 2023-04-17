using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Auth.Model.Enum;

namespace NeZoviReg.Auth.Authorization;

public interface INeZoviRegAuthorizationService : IAuthorizationService
{
    Task<bool> HasPermission(ClaimsPrincipal user, string permission, CancellationToken token = default);
}