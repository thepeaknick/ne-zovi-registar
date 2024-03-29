using NeZoviReg.Domain.Extensions;
using NeZoviReg.Domain.Model.Auth;

#pragma warning disable CS8618

namespace NeZoviReg.Domain.Model.Domain;

/// <summary>
/// Users of the 'Ne_zovi' registry.
/// </summary>
public class RegUser : Entity
{
    public const int CompanyNameMaxLength = 100;
    public const int FirstNameMaxLength = 100;
    public const int LastNameMaxLength = 100;
    public const int AddressMaxLength = 100;
    public const int RegNumberMaxLength = 8;
    public const int TaxNumberMaxLength = 9;
    public const int PasswordMaxLength = 255;
    public const int UsernameMaxLength = 255;
    public const int EmailMaxLength = 50;

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

    public string Email { get; private set; }

    public string FullName => $"Naziv={CompanyName}, Adresa={Address}, MatičniBroj={RegNumber}, Pib={TaxNumber}";

    private readonly List<UserAccount> _userAccounts = new();

    public IReadOnlyCollection<UserAccount> UserAccounts => _userAccounts;

    public string Username { get; private set; }


    public DateTime? AccessTokenExpirationTime { get; private set; }

    public string? RefreshToken { get; private set; }

    public DateTime? RefreshTokenExpirationTime { get; private set; }

    public string? ForgotPasswordToken { get; private set; }

    public DateTime? ForgotPasswordTokenExpirationTime { get; private set; }

    private string _password;

    public string Password
    {
        get => _password.Decode();
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

    public RegUser WithEmail(string? email)
    {
        Email = email ?? Email;

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

        Password = password.Encode();


        return this;
    }

    public RegUser WithUserName(string? userName)
    {
        Username = userName ?? Username;

        return this;
    }

    public RegUser WithAccessTokenExpTime(DateTime? expTime)
    {
        AccessTokenExpirationTime = expTime ?? AccessTokenExpirationTime;

        return this;
    }

    public bool IsAccessTokenValid => AccessTokenExpirationTime != default && DateTime.Now <= AccessTokenExpirationTime;

    public RegUser WithoutAccessTokenExpTime()
    {
        AccessTokenExpirationTime = default;

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

    public RegUser WithForgotPasswordToken(string? token)
    {
        ForgotPasswordToken = token ?? ForgotPasswordToken;

        return this;
    }

    public RegUser WithForgotPasswordTokenExpTime(DateTime? expTime)
    {
        ForgotPasswordTokenExpirationTime = expTime ?? ForgotPasswordTokenExpirationTime;

        return this;
    }

    public RegUser WithUserAccount(UserAccount userAccount)
    {
        var existing = _userAccounts.FirstOrDefault(x => x.Id == userAccount.Id);

        existing?.Delete();

        _userAccounts.Add(userAccount);

        return this;
    }

    public RegUser WithRoles(List<int>? roleIds)
    {
        foreach (var regUserRole in RegUserRoles
                     .Where(r => !roleIds?.Contains(r.RoleId) ?? false)
                     .ToList())
        {
            regUserRole.Delete();
        }

        foreach (var roleId in (roleIds ??= new List<int>())
                 .Where(roleId => RegUserRoles.All(r => r.RoleId != roleId)))
        {
            _regUserRoles.Add(RegUserRole.Create(Id, roleId));
        }

        return this;
    }

    public RegUser WithRole(int? roleId)
    {
        if (roleId == default || _regUserRoles.Any(x => x.RoleId == roleId))
            return this;

        foreach (var regUserRole in RegUserRoles)
        {
            regUserRole.Delete();
        }

        _regUserRoles.Add(RegUserRole.Create(Id, roleId.Value));

        return this;
    }
}