using System.Text;
using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Domain.Model.Domain;

/// <summary>
/// Users of the 'Ne_zovi' registry.
/// </summary>
public class RegUser : Entity
{
    public static int FirstNameMaxLength = 100;
    public static int LastNameMaxLength = 100;
    public static int EmailMaxLength = 10;
    public static int PasswordMaxLength = 255;
    public static int UsernameMaxLength = 255;
    public static int ThumbprintMaxLength = 100;

    public RegUser(string username, string email)
    {
        Username = username;
        Email = email;
    }

    public RegUser(int id, string username, string email)
               : base(id)
    {
        Username = username;
        Email = email;
    }

    public string? FirstName { get; private set; }

    public string? LastName { get; private set; }

    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; private set; }

    public string Username { get; private set; }

    private string? _password;
    public string? Password
    {
        get => Decode(_password ?? string.Empty);
        private set => _password = value;
    }

    public string? ThumbPrint { get; private set; }

    private readonly List<Role> _roles = new();
    public IReadOnlyCollection<Role> Roles => _roles;

    public RegUser AddName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        return this;
    }
    public RegUser AddFirstName(string firstName)
    {
        FirstName = firstName;

        return this;
    }

    public RegUser AddLastName(string lastName)
    {
        LastName = lastName;

        return this;
    }

    public RegUser AddPassword(string password)
    {
        Password = Encode(password);

        return this;
    }

    public RegUser AddRole(Role role)
    {
        _roles.Add(role);

        return this;
    }

    public RegUser AddRoles(List<Role> roles)
    {
        _roles.AddRange(roles);

        return this;
    }


    private static string Encode(string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

    private static string Decode(string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));
}