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
    public const int EmailMaxLength = 50;

    public static RegUser New => new RegUser {GuidId = Guid.NewGuid()};
    
    public static RegUser Create(string companyName)
    {
        return New
            .WithCompanyName(companyName);
    }


    public static RegUser Create(int id, string companyName)
    {
        var regUser = New
            .WithId(id)
            .WithCompanyName(companyName);
        
        regUser.AddCreation();

        return regUser;
    }

    protected override RegUser WithId(int id)
    {
        Id = id;

        return this;
    }

    public Guid GuidId { get; private set; }

    public string CompanyName { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Email { get; private set; }

    public string FullName => $"Naziv={CompanyName}, Adresa={Address}, MatičniBroj={RegNumber}, Pib={TaxNumber}";

    private readonly List<UserAccount> _userAccounts = new();

    public IReadOnlyCollection<UserAccount> UserAccounts => _userAccounts;
    
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
            _regUserRoles.Add(RegUserRole.New(Id, roleId));
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

        _regUserRoles.Add(RegUserRole.New(Id, roleId.Value));

        return this;
    }
}