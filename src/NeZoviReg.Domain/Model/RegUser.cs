namespace NeZoviReg.Domain.Model;

/// <summary>
/// Users of the 'Ne_zovi' registry.
/// </summary>
public class RegUser: Entity
{
    public RegUser(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; private set; }
}