namespace NeZoviReg.Abstractions.Shared.Model;

public class AppUser
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public AppUser(Guid id, string userName)
    {
        Id = id;
        UserName = userName;
    }

    public static AppUser Default => new (Guid.Empty, string.Empty);
}

