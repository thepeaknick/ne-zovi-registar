using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;

namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.User;

public record GetUserQuery : IQuery<UserDto>
{
    private const string SrbCode = "381";
    private const string SrbCodeWithPlus = "+381";
    
    private string _phoneNumber;

    public required string PhoneNumber
    {
        get
        {
            _phoneNumber = _phoneNumber.RemoveSpaces();

            if (_phoneNumber.StartsWith("06"))
            {
                return _phoneNumber.Substring(1, _phoneNumber.Length - 1).Insert(0, SrbCode);
            }

            if (_phoneNumber.StartsWith("6"))
            {
                return _phoneNumber.Insert(0, SrbCode);
            }

            if (_phoneNumber.StartsWith(SrbCodeWithPlus))
            {
                return _phoneNumber.Substring(1, _phoneNumber.Length - 1);
            }

            return _phoneNumber;
        }
        init { _phoneNumber = value; }
    }
}