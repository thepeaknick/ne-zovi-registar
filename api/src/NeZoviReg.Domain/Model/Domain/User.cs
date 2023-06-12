#pragma warning disable CS8618
namespace NeZoviReg.Domain.Model.Domain;

/// <summary>
/// End user, mobile number owner.
/// </summary>
public class User : Entity
{
    public const int FirstNameMaxLength = 100;
    public const int  LastNameMaxLength = 100;
    public const int  PhoneNumberMaxLength = 25;
    public const int  JmbgMaxLength = 13;

    public User()
    {
        CreatedOn = DateTime.Now;
    }

    public User(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    public User(int id, string firstName, string lastName, string phoneNumber)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }


    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string PhoneNumber { get; private set; }

    public string FullName => $"Ime={FirstName}, Prezime={LastName}, Jmbg={Jmbg}, Broj telefona={PhoneNumber}.";

    public string Jmbg { get; private set; } = string.Empty;

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
        Jmbg = jmbg ?? Jmbg;

        return this;
    }

    public User AddPhoneNumber(string? phoneNumber)
    {
        PhoneNumber = phoneNumber ?? PhoneNumber;

        return this;
    }

    public User AddOperator(int? operatorId)
    {
        OperatorId = operatorId ?? OperatorId;

        return this;
    }

    public override string ToString() => $"{FullName}, {Jmbg}";
}