using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NeZoviReg.Auth.Authorization;

public interface INeZoviRegAuthorizationService : IAuthorizationService
{
    Task<bool> HasPermission(ClaimsPrincipal user, string permission, CancellationToken token = default);
}