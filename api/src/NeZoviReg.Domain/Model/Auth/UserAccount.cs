using System.Dynamic;
using NeZoviReg.Domain.Extensions;
using NeZoviReg.Domain.Model.Domain;

#pragma warning disable CS8618

namespace NeZoviReg.Domain.Model.Auth;

public class UserAccount : Entity
{
    public const int UsernameMaxLength = 255;
    public const int PasswordMaxLength = 255;
    public const int EmailMaxLength = 50;

    public static UserAccount New => new UserAccount {GuidId = Guid.NewGuid()};

    public static UserAccount Create(int id, string username, string password)
    {
        var userAccount = New
            .WithId(id)
            .WithUserName(username)
            .WithPassword(password);
        userAccount.AddCreation();

        return userAccount;
    }


    public Guid GuidId { get; private set; }

    public int RegUserId { get; private set; }
    
    public RegUser RegUser { get; private set; }
    public string Username { get; private set; }

    private string _password;

    public string Password
    {
        get => _password.Decode();
        private set => _password = value;
    }

    public DateTime? AccessTokenExpirationTime { get; private set; }

    public string? RefreshToken { get; private set; }

    public DateTime? RefreshTokenExpirationTime { get; private set; }

    public string? ForgotPasswordToken { get; private set; }

    public DateTime? ForgotPasswordTokenExpirationTime { get; private set; }

    protected override UserAccount WithId(int id)
    {
        Id = id;

        return this;
    }

    public UserAccount WithPassword(string? password)
    {
        if (password == default)
            return this;

        Password = password.Encode();


        return this;
    }

    public UserAccount WithUserName(string? userName)
    {
        Username = userName ?? Username;

        return this;
    }

    public UserAccount WithRegUserId(int regUserId)
    {
        RegUserId = regUserId;

        return this;
    }

    public UserAccount WithAccessTokenExpTime(DateTime? expTime)
    {
        AccessTokenExpirationTime = expTime ?? AccessTokenExpirationTime;

        return this;
    }

    public bool IsAccessTokenValid => AccessTokenExpirationTime != default && DateTime.Now <= AccessTokenExpirationTime;

    public UserAccount WithoutAccessTokenExpTime()
    {
        AccessTokenExpirationTime = default;

        return this;
    }

    public UserAccount WithRefreshToken(string? refreshToken)
    {
        RefreshToken = refreshToken ?? RefreshToken;

        return this;
    }

    public UserAccount WithoutRefreshToken()
    {
        RefreshToken = default;
        RefreshTokenExpirationTime = default;

        return this;
    }

    public UserAccount WithRefreshTokenExpTime(DateTime? expTime)
    {
        RefreshTokenExpirationTime = expTime ?? RefreshTokenExpirationTime;

        return this;
    }

    public UserAccount WithForgotPasswordToken(string? token)
    {
        ForgotPasswordToken = token ?? ForgotPasswordToken;

        return this;
    }

    public UserAccount WithForgotPasswordTokenExpTime(DateTime? expTime)
    {
        ForgotPasswordTokenExpirationTime = expTime ?? ForgotPasswordTokenExpirationTime;

        return this;
    }
}