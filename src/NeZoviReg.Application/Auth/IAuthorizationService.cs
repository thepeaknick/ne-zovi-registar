using NeZoviReg.Domain.Model.Auth.Enum;
using System.Security.Claims;

namespace NeZoviReg.Application.Auth;

public interface IAuthorizationService
{
    Task<bool> HasRole(ClaimsPrincipal user, RoleType roleType, CancellationToken token = default);

    Task<bool> HasPermission(ClaimsPrincipal user, string permission, CancellationToken token = default);
}