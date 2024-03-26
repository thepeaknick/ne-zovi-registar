using AutoMapper;
using NeZoviReg.Abstractions.Shared.Model.Infrastructure;
using NeZoviReg.WebClient.PlService;

namespace NeZoviReg.WebClient.Apr;

public class AprNeZoviRegMappingProfile : Profile
{
    public AprNeZoviRegMappingProfile()
    {
        CreateMap<PrivredniSubjekat, AprBusinessEntity>()
            .ForMember(d => d.RegNumber, o =>
            {
                o.Condition(s => s.grupa.Any());
                o.MapFrom((s, _) =>
                    s.grupa.FirstOrDefault(x => x.id == "1001")?.podatak.FirstOrDefault(p => p.naziv == "MaticniBroj")
                        ?.vrednost);
            })
            .ForMember(d => d.CompanyName, o =>
            {
                o.Condition(s => s.grupa.Any());
                o.MapFrom((s, _) =>
                    s.grupa.FirstOrDefault(x => x.id == "1006")?.podatak
                        .FirstOrDefault(p => p.naziv == "SkracenoPoslovnoIme")?.vrednost);
            })
            .ForMember(d => d.FirstName, o =>
            {
                o.Condition(s => s.grupa.Any());
                o.MapFrom((s, _) =>
                    s.grupa.FirstOrDefault(x => x.id == "1027")?.podatak.FirstOrDefault(p => p.naziv == "Ime")
                        ?.vrednost);
            })
            .ForMember(d => d.LastName, o =>
            {
                o.Condition(s => s.grupa.Any());
                o.MapFrom((s, _) =>
                    s.grupa.FirstOrDefault(x => x.id == "1027")?.podatak.FirstOrDefault(p => p.naziv == "Prezime")
                        ?.vrednost);
            })
            .ForMember(d => d.TaxNumber, o =>
            {
                o.Condition(s => s.grupa.Any());
                o.MapFrom((s, _) =>
                    s.grupa.FirstOrDefault(x => x.id == "1017")?.podatak.FirstOrDefault(p => p.naziv == "PIB")
                        ?.vrednost);
            })
            .ForMember(d => d.Email, o =>
            {
                o.Condition(s => s.grupa.Any());
                o.MapFrom((s, _) =>
                    s.grupa.FirstOrDefault(x => x.id == "1013")?.podatak.FirstOrDefault(p => p.naziv == "EMailAdresa")
                        ?.vrednost);
            })
            .ForMember(d => d.TheAddress, o =>
            {
                o.Condition(s => s.grupa.Any());
                o.MapFrom((s, _) => s.grupa.FirstOrDefault(x => x.id == "1011"));
            });

        CreateMap<Grupa, Address>()
            .ConstructUsing(x => new Address())
            .ForMember(s => s.Street,
                o => o.MapFrom((s, _) => s.podatak.FirstOrDefault(p => p.naziv == "NazivUlice")?.vrednost))
            .ForMember(s => s.StreetNumber,
                o => o.MapFrom((s, _) => s.podatak.FirstOrDefault(p => p.naziv == "AdresaBroj")?.vrednost))
            .ForMember(s => s.FloorNumber,
                o => o.MapFrom((s, _) => s.podatak.FirstOrDefault(p => p.naziv == "AdresaSprat")?.vrednost))
            .ForMember(s => s.ApartmentNumber,
                o => o.MapFrom((s, _) => s.podatak.FirstOrDefault(p => p.naziv == "AdresaBrojStana")?.vrednost));
    }
}