using NeZoviReg.Domain.Extensions;

#pragma warning disable CS8618
namespace NeZoviReg.Domain.Model.Domain;

/// <summary>
/// End user, mobile number owner.
/// </summary>
public class User : Entity
{
    public const string PhoneNumberRegex = "^381|06[0-9]{1}[0-9]{6,7}$";
    public const int FirstNameMaxLength = 100;
    public const int  LastNameMaxLength = 100;
    public const int  PhoneNumberMaxLength = 25;
    public const int  JmbgMaxLength = 13;

    public User()
    {
        CreatedOn = DateTime.Now;
    }

    public User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public User(int id, string firstName, string lastName)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
    }


    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string PhoneNumber { get; private set; }

    public bool Active { get; private set; } = true;

    public string FullName => $"Ime={FirstName}, Prezime={LastName}, Jmbg={Jmbg}, Broj telefona={PhoneNumber}.";

    private string _jmbg;
    public string Jmbg
    {
        get => _jmbg;//.Decrypt();
        private set => _jmbg = value;
    }

    public RegUser Operator { get; private set; }
    public int OperatorId { get; private set; }

    public User AddFirstName(string? firstName)
    {
        FirstName = firstName ?? FirstName;

        return this;
    }

    public User AddLastName(string? lastName)
    {
        LastName = lastName ?? LastName;

        return this;
    }

    public User AddName(string? firstName, string? lastName)
    {
        AddFirstName(firstName);
        AddLastName(lastName);

        return this;
    }

    public User AddJmbg(string? jmbg)
    {
        if (jmbg == default)
            return this;
        
        Jmbg = jmbg.Encrypt();

        return this;
    }

    public User AddPhoneNumber(string? phoneNumber)
    {
        PhoneNumber = phoneNumber ?? PhoneNumber.FormatPhoneNumber();

        return this;
    }

    public User AddOperator(int? operatorId)
    {
        OperatorId = operatorId ?? OperatorId;

        return this;
    }

    public User Deactivate()
    {
        Active = false;

        return this;
    }

    public bool IsActive => Active;

    public override string ToString() => $"{FullName}, {Jmbg}";
}