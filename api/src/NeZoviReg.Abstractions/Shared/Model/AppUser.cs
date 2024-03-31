using System.Security.Claims;
using System.Security.Principal;
using NeZoviReg.Abstractions.Shared.Model.Auth;

namespace NeZoviReg.Abstractions.Shared.Model;

public class AppUser
{
    public Guid UserAccountId { get; set; }
    
    public Guid RegUserId { get; set; }

    public string UserName { get; set; }

    public AppUser(Guid userAccountId, string userName, Guid regUserId)
    {
        UserAccountId = userAccountId;
        UserName = userName;
        RegUserId = regUserId;
    }

    public static AppUser Default => new(default, string.Empty, default);

    public static AppUser GetUser(ClaimsPrincipal principal)
    {
        if (!Guid.TryParse(principal.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId)?.Value,
                out Guid userId) || !Guid.TryParse(principal.Claims.FirstOrDefault(x => x.Type == CustomClaims.RegUserId)?.Value,
                out Guid regUserId))
            return AppUser.Default;

        var userName = principal.Claims
            .FirstOrDefault(x => x.Type == CustomClaims.UserName)?
            .Value ?? string.Empty;

        return new(userId, userName, regUserId);

    }
    
    public static string? GetUserName(IIdentity? identity)
    {
        return ((ClaimsIdentity)identity)?.Claims
                .FirstOrDefault(x => x.Type == CustomClaims.UserName)?
                .Value ?? default;
    }
}

