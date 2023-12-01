using AutoMapper;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Shared.Model.Domain;
using NeZoviReg.Domain.Extensions;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Application.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ConstructUsing(s => new UserDto(s.PhoneNumber, s.ModifiedOn ?? s.CreatedOn));
        
        CreateMap<User, UserInfoDto>()
            .ForMember(d => d.RegisteredOn, o => o.MapFrom(s => s.CreatedOn))
            .ForMember(d => d.Active, o => o.MapFrom(s => s.IsActive))
            .ForMember(d => d.RemovedOn, o => o.MapFrom(s => !s.IsActive ? s.ModifiedOn : default))
            .ForMember(d => d.Operator, o => o.MapFrom(s => s.Operator.CompanyName))
            .ForMember(d => d.OperatorGuid, o => o.MapFrom(s => s.Operator.GuidId));

        CreateMap<User, UserDetailsDto>()
            .ConstructUsing(s => new UserDetailsDto(s.PhoneNumber, s.FirstName, s.LastName, s.Jmbg, s.OperatorId));

        CreateMap<BulkUser, User>()
            .AfterMap((s,d,c) =>
            {
                d.AddJmbg(s.Jmbg);
            })
            .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber.FormatPhoneNumber()));

        CreateMap<RegUser, RegUserDto>()
            .ConstructUsing(s => new RegUserDto(s.GuidId, s.CompanyName, s.RegNumber, s.TaxNumber, s.CreatedOn, s.Id));

        CreateMap<RegUser, RegUserDetailsDto>()
            .ConstructUsing(s => new RegUserDetailsDto(s.GuidId, s.FirstName, s.LastName, s.Email, s.CompanyName,
                s.Address,
                s.RegNumber, s.TaxNumber, s.Username, s.RegUserRoles.First().RoleId));
    }
}