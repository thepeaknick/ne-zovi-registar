using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Auth.Model.Enum;

namespace NeZoviReg.Auth;

public interface INeZoviRegAuthorizationService : IAuthorizationService
{
    Task<bool> HasRole(ClaimsPrincipal user, RoleType roleType, CancellationToken token = default);
}