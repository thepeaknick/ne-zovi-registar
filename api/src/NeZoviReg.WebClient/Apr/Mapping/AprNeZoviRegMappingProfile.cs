using AutoMapper;
using NeZoviReg.Abstractions.Shared.Model.AprBusinessEntity;
using NeZoviReg.WebClient.PlService;

namespace NeZoviReg.WebClient.Apr;

public class AprNeZoviRegMappingProfile :Profile
{
    public AprNeZoviRegMappingProfile()
    {
        CreateMap<PrivredniSubjekat, AprBusinessEntity>()
            .ForMember(d=>d.RegNumber, o=>o.MapFrom(s=>s.maticniBroj));
    }
}