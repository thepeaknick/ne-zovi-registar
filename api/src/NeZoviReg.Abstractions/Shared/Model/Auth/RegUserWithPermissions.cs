using NeZoviReg.Domain.Model.Domain;
#pragma warning disable CS8618

namespace NeZoviReg.Abstractions.Shared.Model.Auth;

public class RegUserWithPermissions
{
    public RegUserWithPermissions()
    {
    }

    public RegUser RegUser { get; init; }

    public List<string>? Permissions  { get; init; }
}