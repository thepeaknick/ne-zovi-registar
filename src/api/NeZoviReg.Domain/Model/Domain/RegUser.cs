using System.Text;
using NeZoviReg.Domain.Model.Auth;
#pragma warning disable CS8618

namespace NeZoviReg.Domain.Model.Domain;

/// <summary>
/// Users of the 'Ne_zovi' registry.
/// </summary>
public class RegUser : Entity
{
    public static int FirstNameMaxLength = 100;
    public static int LastNameMaxLength = 100;
    public static int EmailMaxLength = 100;
    public static int PasswordMaxLength = 255;
    public static int UsernameMaxLength = 255;
    public static int ThumbprintMaxLength = 100;

    public RegUser()
    :base()
    {
    }

    public RegUser(string username, string email)
    {
        Username = username;
        Email = email;
        GuidId = Guid.NewGuid();
    }

    public RegUser(int id, string username, string email)
               : base(id)
    {
        Username = username;
        Email = email;
        GuidId = Guid.NewGuid();
    }

    public Guid GuidId { get; private set; }

    public string? FirstName { get; private set; }

    public string? LastName { get; private set; }

    public string FullName => $"Email adresa={Email}, Korisničko ime={Username}, Ime={FirstName}, Prezime={LastName}";

    public string Email { get; private set; }

    public string Username { get; private set; }

    private string? _password;
    public string? Password
    {
        get => Decode(_password ?? string.Empty);
        private set => _password = value;
    }

    public string? ThumbPrint { get; private set; }

    private readonly List<RegUserRole> _regUserRoles = new();
    public IReadOnlyCollection<RegUserRole> RegUserRoles => _regUserRoles;

    public RegUser AddEmail(string? email)
    {
        Email = email ?? Email;

        return this;
    }

    public RegUser AddFirstName(string? firstName)
    {
        FirstName = firstName ?? FirstName;

        return this;
    }

    public RegUser AddLastName(string? lastName)
    {
        LastName = lastName ?? LastName;

        return this;
    }

    public RegUser AddName(string? firstName, string? lastName)
    {
        AddFirstName(firstName);
        AddLastName(lastName);

        return this;
    }

    public RegUser AddPassword(string? password)
    {
        if (password == default)
            return this;

        Password = Encode(password);

        return this;
    }

    public RegUser AddRole(int roleId)
    {
        _regUserRoles.Add(RegUserRole.Create(Id, roleId));

        return this;
    }

    public RegUser AddRoles(List<int>? roleIds)
    {
        foreach (var regUserRole in RegUserRoles
                     .Where(r => !roleIds?.Contains(r.RoleId) ?? false)
                     .ToList())
        {
            regUserRole.PrepareForDelete();
        }

        foreach (var roleId in (roleIds ??= new List<int>())
                 .Where(roleId => RegUserRoles.All(r => r.RoleId != roleId)))
        {
            _regUserRoles.Add(RegUserRole.Create(Id, roleId));
        }

        return this;
    }

    private static string Encode(string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

    private static string Decode(string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));
}