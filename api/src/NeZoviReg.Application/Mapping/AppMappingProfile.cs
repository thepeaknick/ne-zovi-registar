using AutoMapper;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Shared.Model.Domain;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Application.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ConstructUsing(s => new UserDto(s.PhoneNumber, s.ModifiedOn ?? s.CreatedOn));

        CreateMap<BulkUser, User>();

        CreateMap<RegUser, RegUserDto>()
            .ConstructUsing(s => new RegUserDto(s.GuidId, s.CompanyName, s.RegNumber, s.TaxNumber, s.CreatedOn, s.Id));

        CreateMap<RegUser, RegUserDetailsDto>()
            .ConstructUsing(s => new RegUserDetailsDto(s.GuidId, s.FirstName, s.LastName, s.CompanyName, s.Address,
                s.RegNumber, s.TaxNumber, s.RegUserRoles.First().RoleId));
    }
}