namespace NeZoviReg.Domain.Model;

/// <summary>
/// End user, mobile number owner.
/// </summary>
public class User : Entity
{
    public User(string firstName, string lastName, string phoneNumber, string jmbg)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Jmbg = jmbg;
    }

    public User(int id, string firstName, string lastName, string phoneNumber, string jmbg)
        :base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Jmbg = jmbg;
    }


    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string PhoneNumber { get; private set; }

    public string FullName => $"{FirstName} {LastName}";

    public string Jmbg { get; private set; }

    public override string ToString() => $"{FullName}, {Jmbg}";
}