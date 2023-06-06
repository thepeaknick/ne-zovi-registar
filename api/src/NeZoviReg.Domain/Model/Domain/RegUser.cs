using System.Text;
using NeZoviReg.Domain.Model.Auth;
#pragma warning disable CS8618

namespace NeZoviReg.Domain.Model.Domain;

/// <summary>
/// Users of the 'Ne_zovi' registry.
/// </summary>
public class RegUser : Entity
{
    public static int CompanyNameMaxLength = 100;
    public static int FirstNameMaxLength = 100;
    public static int LastNameMaxLength = 100;
    public static int AddressMaxLength = 100;
    public static int RegNumberMaxLength = 8;
    public static int TaxNumberMaxLength = 9;
    public static int PasswordMaxLength = 255;
    public static int UsernameMaxLength = 255;

    public RegUser()
    : base()
    {
    }

    public RegUser(string companyName, string userName)
    {
        CompanyName = companyName;
        Username = userName;
        GuidId = Guid.NewGuid();
    }

    public RegUser(int id, string companyName, string userName)
               : base(id)
    {
        CompanyName = companyName;
        Username = userName;
        GuidId = Guid.NewGuid();
    }

    public Guid GuidId { get; private set; }

    public string CompanyName { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string FullName => $"Naziv={CompanyName}, Adresa={Address}, MatičniBroj={RegNumber}, Pib={TaxNumber}";

    public string Username { get; private set; }

    public string? RefreshToken { get; private set; }

    public DateTime? RefreshTokenExpirationTime { get; private set; }

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

    public RegUser WithAddress(string? address)
    {
        Address = address ?? Address;

        return this;
    }

    public RegUser WithRegNumber(string? regNumb)
    {
        RegNumber = regNumb ?? RegNumber;

        return this;
    }

    public RegUser WithTaxNumber(string? taxNumber)
    {
        TaxNumber = taxNumber ?? TaxNumber;

        return this;
    }

    public RegUser WithFirstName(string? firstName)
    {
        FirstName = firstName ?? FirstName;

        return this;
    }

    public RegUser WithLastName(string? lastName)
    {
        LastName = lastName ?? LastName;

        return this;
    }

    public RegUser WithName(string? firstName, string? lastName)
    {
        WithFirstName(firstName);
        WithLastName(lastName);

        return this;
    }

    public RegUser WithCompanyName(string? name)
    {
        CompanyName = name ?? CompanyName;

        return this;
    }

    public RegUser WithPassword(string? password)
    {
        if (password == default)
            return this;

        Password = Encode(password);

        return this;
    }

    public RegUser WithUserName(string? userName)
    {
        Username = userName ?? Username;

        return this;
    }

    public RegUser WithRole(int roleId)
    {
        _regUserRoles.Add(RegUserRole.Create(Id, roleId));

        return this;
    }

    public RegUser WithRefreshToken(string? refreshToken)
    {
        RefreshToken = refreshToken ?? RefreshToken;

        return this;
    }

    public RegUser WithoutRefreshToken()
    {
        RefreshToken = default;
        RefreshTokenExpirationTime = default;

        return this;
    }

    public RegUser WithRefreshTokenExpTime(DateTime? expTime)
    {
        RefreshTokenExpirationTime = expTime ?? RefreshTokenExpirationTime;

        return this;
    }

    public RegUser WithRoles(List<int>? roleIds)
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