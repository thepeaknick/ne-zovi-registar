using AutoMapper;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Application.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ConstructUsing(s => new UserDto(s.PhoneNumber, s.ModifiedOn != null ? $"{s.ModifiedOn:dd.MM.yy HH:mm}" : $"{s.CreatedOn:dd.MM.yy HH:mm}"));

        CreateMap<RegUser, RegUserDto>()
            .ConstructUsing(s => new RegUserDto(s.GuidId, s.CompanyName, s.Id));

        CreateMap<RegUser, RegUserDetailsDto>()
            .ConstructUsing(s => new RegUserDetailsDto(s.GuidId, s.FirstName, s.LastName, s.CompanyName, s.Address, s.RegNumber, s.TaxNumber, s.RegUserRoles.First().RoleId));
    }
}