using AutoMapper;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Application.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ConstructUsing(s => new UserDto(s.PhoneNumber, s.ModifiedOn ?? s.CreatedOn));

        CreateMap<RegUser, RegUserDto>()
            .ConstructUsing(s => new RegUserDto(s.GuidId, s.CompanyName, s.Id));
    }
}