using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Extensions.Domain;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;

namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.User;

public record GetUserQuery : IQuery<UserDto>
{
    
    
    private string _phoneNumber;

    public required string PhoneNumber
    {
        get
        {
            _phoneNumber = _phoneNumber.RemoveSpaces();

            return _phoneNumber.FormatPhoneNumber();
        }
        init { _phoneNumber = value; }
    }
}