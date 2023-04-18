using NeZoviReg.Domain.Shared;
using NeZoviReg.Domain.Shared.Enums;

namespace NeZoviReg.Domain.Errors;

public static class DomainErrors
{
    public static class RegUser
    {
        public static readonly Func<string, Error> EmailAlreadyInUse = email =>  new(
            ErrorCode.EmailAlreadyInUse,
            $"E-mail adresa '{email}' je već u upotrebi.");

        public static readonly Func<int, Error> NotFound = id => new Error(
            ErrorCode.NotFound,
            $"Korisnik {id} nije pronađen.");

        public static readonly Error InvalidCredentials = new(
            ErrorCode.InvalidCredentials,
            "Korisničko ime/lozinka nisu ispravni.");
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
}
