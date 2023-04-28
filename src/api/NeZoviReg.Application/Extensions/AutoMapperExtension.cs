using AutoMapper;
using NeZoviReg.Application.Mapping;

namespace NeZoviReg.Application.Extensions;

public static class AutoMapperExtension
{
    public static Action<IMapperConfigurationExpression> AddApplicationProfile
    {
        get { return c => c.AddProfile<AppMappingProfile>(); }
    }
}