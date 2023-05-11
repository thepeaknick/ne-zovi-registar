using NeZoviReg.Abstractions.Shared.Enums;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.Abstractions.Shared.Errors;

public static class RegErrors
{
    public static class App
    {
        public static readonly Func<int?, Error> RateLimitRejected = retry =>  new(
            ErrorCode.Rejected,
            retry == default
                ? "Previše pokušaja. Molimo vas pokušajte kasnije."
                : $"Previše pokušaja. Molimo vas pokušajte za {retry} minut/a.");

        public static readonly Error InternalServerError = new(
            ErrorCode.InternalServerError,
            "Desila se greška na serveru.");

        public static readonly Error ForbiddenAccess = new(
            ErrorCode.Forbidden,
            "Korisnik nije autorizovan za traženi zahtev.");

    }

    public static class RegUser
    {
        public static readonly Func<dynamic, Error> NotFound = ident => new Error(
            ErrorCode.NotFound,
            $"Korisnik registra '{ident}' nije pronađen.");

       public static readonly Func<RoleType, Error> RoleNotFound = role => new Error(
            ErrorCode.NotFound,
            $"{role} nije pronađen.");

       public static readonly Func<string, Error> RolesNotFound = role => new Error(
           ErrorCode.NotFound,
           $"{role} nisu pronađeni.");

        public static readonly Error InvalidCredentials = new(
            ErrorCode.InvalidCredentials,
            "Korisničko ime/lozinka nisu ispravni.");

        public static readonly Error IdentificatorEmpty = new(
            ErrorCode.Empty,
            "Identifikator korisnika je prazan.");

        public static readonly Error NotRegistered = new(
            ErrorCode.NotFound,
            "Korisnik nije registrovan.");
    }

    public static class Operator
    {
       public static readonly Error Empty = new(
            ErrorCode.Empty,
            "Operator je obavezan.");
    }

    public static class CompanyName
    {
        public static readonly Func<string, Error> AlreadyInUse = name =>  new(
            ErrorCode.AlreadyInUse,
            $"Naziv '{name}' je već u upotrebi.");

        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "Naziv je obavezan.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "Naziv je predugačak.");
    }

    public static class Address
    {
        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "Adresa je obavezna.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "Adresa je predugačka.");
    }
    public static class UserName
    {
        public static readonly Func<string, Error> AlreadyInUse = username =>  new(
            ErrorCode.AlreadyInUse,
            $"Korisničko ime '{username}' je već u upotrebi.");

        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "Korisničko ime je obavezno.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "Korisničko ime je predugačko.");
    }

    public static class Password
    {
        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "Lozinka je obavezna.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "Lozinka je predugačka.");
    }

    public static class RegNumber
    {
        public static readonly Func<string, Error> AlreadyInUse = regNumber =>  new(
            ErrorCode.AlreadyInUse,
            $"Matični broj '{regNumber}' je već u upotrebi.");

        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "Matični broj je obavezan.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "Matični broj je predugačak.");
    }

    public static class TaxNumber
    {
        public static readonly Func<string, Error> AlreadyInUse = taxNumber =>  new(
            ErrorCode.AlreadyInUse,
            $"PIB '{taxNumber}' je već u upotrebi.");

        public static readonly Error Empty = new(
            ErrorCode.Empty,
            "PIB je obavezan.");

        public static readonly Error TooLong = new(
            ErrorCode.TooLong,
            "PIB je predugačak.");
    }

    public static class User
    {
        public static readonly Func<string, Error> NotFound = phoneNumber => new(
            ErrorCode.NotFound,
            $"Broj telefona '{phoneNumber}' ne postoji u registru.");

        public static readonly Func<DateTime?, Error> NotFoundAfter = after=>
        {
            var afterStr = after == default ? string.Empty : $" posle {after:d/M/yy}";
            return new(ErrorCode.NotFound,
                $"Nema novih korisnika u registru{afterStr}.");
        };
    }

    public static class Email
    {
        public static readonly Func<string, Error> AlreadyInUse = email =>  new(
            ErrorCode.AlreadyInUse,
            $"E-mail adresa '{email}' je već u upotrebi.");

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
            "Format Broj telefona nije ispravan.");

        public static readonly Func<string, Error> AlreadyInUse = phone =>  new(
            ErrorCode.AlreadyInUse,
            $"Korisnik sa brojem '{phone}' je već u registru.");
    }
}
