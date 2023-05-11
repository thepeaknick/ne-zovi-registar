using System.Text;
using NeZoviReg.Domain.Model.Auth;
#pragma warning disable CS8618

namespace NeZoviReg.Domain.Model.Domain;

/// <summary>
/// Users of the 'Ne_zovi' registry.
/// </summary>
public class RegUser : Entity
{
    public static int NameMaxLength = 100;
    public static int AddressMaxLength = 100;
    public static int RegNumberMaxLength = 8;
    public static int TaxNumberMaxLength = 9;
    public static int PasswordMaxLength = 255;
    public static int UsernameMaxLength = 255;

    public RegUser()
    : base()
    {
    }

    public RegUser(string name, string userName)
    {
        Name = name;
        Username = userName;
        GuidId = Guid.NewGuid();
    }

    public RegUser(int id, string name, string userName)
               : base(id)
    {
        Name = name;
        Username = userName;
        GuidId = Guid.NewGuid();
    }

    public Guid GuidId { get; private set; }

    public string Name { get; private set; }

    public string FullName => $"Naziv={Name}, Adresa={Address}, MatičniBroj={RegNumber}, Pib={TaxNumber}";

    public string Username { get; private set; }

    private string? _password;
    public string? Password
    {
        get => Decode(_password ?? string.Empty);
        private set => _password = value;
    }

    public string Address { get; private set; }

    public string RegNumber { get; private set; }

    public string TaxNumber { get; private set; }

    private readonly List<RegUserRole> _regUserRoles = new();
    public IReadOnlyCollection<RegUserRole> RegUserRoles => _regUserRoles;

    public RegUser AddAddress(string? address)
    {
        Address = address ?? Address;

        return this;
    }

    public RegUser AddRegNumber(string? regNumb)
    {
        RegNumber = regNumb ?? RegNumber;

        return this;
    }

    public RegUser AddTaxNumber(string? taxNumber)
    {
        TaxNumber = taxNumber ?? TaxNumber;

        return this;
    }

    public RegUser AddName(string? name)
    {
        Name = name ?? Name;

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

    public static string Encode(string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

    public static string Decode(string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));
}