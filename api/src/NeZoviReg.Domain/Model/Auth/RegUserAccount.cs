using System.Dynamic;
using NeZoviReg.Domain.Extensions;
using NeZoviReg.Domain.Model.Domain;

#pragma warning disable CS8618

namespace NeZoviReg.Domain.Model.Auth;

public class RegUserAccount : Entity
{
    public const int UsernameMaxLength = 255;
    public const int PasswordMaxLength = 255;
    public const int EmailMaxLength = 50;
    public const int FirstNameMaxLength = 100;
    public const int LastNameMaxLength = 100;

    public static RegUserAccount New => new RegUserAccount {GuidId = Guid.NewGuid()};

    public static RegUserAccount Create(int id, string username, string password)
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
    
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

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

    protected override RegUserAccount WithId(int id)
    {
        Id = id;

        return this;
    }

    public RegUserAccount WithPassword(string? password)
    {
        if (password == default)
            return this;

        Password = password.Encode();


        return this;
    }

    public RegUserAccount WithUserName(string? userName)
    {
        Username = userName ?? Username;

        return this;
    }
    
    public RegUserAccount WithFirstName(string? firstName)
    {
        FirstName = firstName ?? FirstName;

        return this;
    }

    public RegUserAccount WithLastName(string? lastName)
    {
        LastName = lastName ?? LastName;

        return this;
    }

    public RegUserAccount WithRegUserId(int regUserId)
    {
        RegUserId = regUserId;

        return this;
    }

    public RegUserAccount WithAccessTokenExpTime(DateTime? expTime)
    {
        AccessTokenExpirationTime = expTime ?? AccessTokenExpirationTime;

        return this;
    }

    public bool IsAccessTokenValid => AccessTokenExpirationTime != default && DateTime.Now <= AccessTokenExpirationTime;

    public RegUserAccount WithoutAccessTokenExpTime()
    {
        AccessTokenExpirationTime = default;

        return this;
    }

    public RegUserAccount WithRefreshToken(string? refreshToken)
    {
        RefreshToken = refreshToken ?? RefreshToken;

        return this;
    }

    public RegUserAccount WithoutRefreshToken()
    {
        RefreshToken = default;
        RefreshTokenExpirationTime = default;

        return this;
    }

    public RegUserAccount WithRefreshTokenExpTime(DateTime? expTime)
    {
        RefreshTokenExpirationTime = expTime ?? RefreshTokenExpirationTime;

        return this;
    }

    public RegUserAccount WithForgotPasswordToken(string? token)
    {
        ForgotPasswordToken = token ?? ForgotPasswordToken;

        return this;
    }

    public RegUserAccount WithForgotPasswordTokenExpTime(DateTime? expTime)
    {
        ForgotPasswordTokenExpirationTime = expTime ?? ForgotPasswordTokenExpirationTime;

        return this;
    }
    
    public RegUserAccount Clone()
    {
        var newAcc = (RegUserAccount)this.MemberwiseClone();
        newAcc.Id = 0;

        return newAcc;
    }
}