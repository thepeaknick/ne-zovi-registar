using AutoMapper;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Application.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<RegUser, RegUserDto>()
            .ConstructUsing(s => new RegUserDto(s.Id, s.GuidId, s.CompanyName));
    }
}