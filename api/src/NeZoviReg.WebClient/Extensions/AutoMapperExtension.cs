using AutoMapper;
using NeZoviReg.WebClient.Apr;

namespace NeZoviReg.WebClient.Extensions;

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