using System.Security.Claims;
using System.Security.Principal;
using NeZoviReg.Abstractions.Shared.Model.Auth;

namespace NeZoviReg.Abstractions.Shared.Model;

public class AppUser
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public AppUser(Guid id, string userName)
    {
        Id = id;
        UserName = userName;
    }

    public static AppUser Default => new(default, string.Empty);

    public static AppUser GetUser(ClaimsPrincipal principal)
    {
        if (!Guid.TryParse(principal.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId)?.Value,
                out Guid userId))
            return AppUser.Default;

        var userName = principal.Claims
            .FirstOrDefault(x => x.Type == CustomClaims.UserName)?
            .Value ?? string.Empty;

        return new(userId, userName);

    }
    
    public static string? GetUserName(IIdentity? identity)
    {
        return ((ClaimsIdentity)identity)?.Claims
                .FirstOrDefault(x => x.Type == CustomClaims.UserName)?
                .Value ?? default;
    }
}

