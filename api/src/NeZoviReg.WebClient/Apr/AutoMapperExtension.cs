using AutoMapper;
using NeZoviReg.Application.Mapping;
using NeZoviReg.WebClient.Apr;

namespace NeZoviReg.Application.Extensions;

public static class WebClientAutoMapperExtension
{
    public static Action<IMapperConfigurationExpression> AddMappingProfiles
    {
        get
        {
            return c =>
            {
                c.AddProfile<AprNeZoviRegMappingProfile>();
                // keep adding here
            }; 
        }
         
    }
}