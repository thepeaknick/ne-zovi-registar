using NeZoviReg.Abstractions.Shared.Enums;

namespace NeZoviReg.Abstractions.Shared.Errors;

public static class ValidationErrors
{
    public static class RegUser
    {
        public static readonly Func<string, Error> EmailAlreadyInUse = email =>  new(
            ErrorCode.EmailAlreadyInUse,
            $"E-mail adresa '{email}' je već u upotrebi.");

        public static readonly Func<dynamic, Error> NotFound = identificator => new Error(
            ErrorCode.NotFound,
            $"Korisnik '{identificator}' nije pronađen.");

        public static readonly Error InvalidCredentials = new(
            ErrorCode.InvalidCredentials,
            "Korisničko ime/lozinka nisu ispravni.");

        public static readonly Error UserNameTooLong = new(
            ErrorCode.TooLong,
            "Korisničko ime je predugačko.");

        public static readonly Error PasswordTooLong = new(
            ErrorCode.TooLong,
            "Lozinka je predugačka.");
    }

    public static class Email
    {
        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "Email adresa je prazna.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "Email adresa je predugačka.");

        public static readonly Error InvalidFormat = new(
            ErrorCode.InvalidFormat,
            "Email format nije ispravan.");
    }

    public static class FirstName
    {
        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "Ime je prazno.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "Ime je predugačko.");
    }

    public static class LastName
    {
        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "Prezime je prazno.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "Prezime je predugačko.");
    }

    public static class Jmbg
    {
        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "JMBG je prazno.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "JMBG je predugačko.");
    }

    public static class PhoneNumber
    {
        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "Broj telefona je prazan.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "Broj telefona je predugačak.");

        public static readonly Error InvalidFormat = new(
            ErrorCode.InvalidFormat,
            "Broj telefona format nije ispravan.");
    }
}
