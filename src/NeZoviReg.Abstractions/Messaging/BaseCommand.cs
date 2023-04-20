namespace NeZoviReg.Abstractions.Messaging;

public record BaseCommand : ICommand
{
    public ICommand AddAppUser(string appUser)
    {
        AppUser = appUser;

        return this;
    }

    public string AppUser { get; protected set; } = string.Empty;
}

public record BaseCommand<TResult> : ICommand<TResult>
{
    public ICommand<TResult>AddAppUser(string appUser)
    {
        AppUser = appUser;

        return this;
    }

    public string AppUser { get; protected set; } = string.Empty;
}