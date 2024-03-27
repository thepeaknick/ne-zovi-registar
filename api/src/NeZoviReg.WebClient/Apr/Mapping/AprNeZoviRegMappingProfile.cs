using AutoMapper;
using NeZoviReg.Abstractions.Shared.Model.Infrastructure;
using NeZoviReg.WebClient.PlService;

namespace NeZoviReg.WebClient.Apr;

public class AprNeZoviRegMappingProfile : Profile
{
    public AprNeZoviRegMappingProfile()
    {
        CreateMap<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity>()
            .ForMember(d => d.RegNumber, o =>
            {
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item1 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    s.AprData.grupa.FirstOrDefault(x => x.id == "1001")?.podatak.FirstOrDefault(p => p.naziv == "MaticniBroj")
                        ?.vrednost);
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item2 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    s.AprData.grupa.FirstOrDefault(x => x.id == "1041")?.podatak.FirstOrDefault(p => p.naziv == "MaticniBroj")
                        ?.vrednost);
                
            })
            .ForMember(d => d.CompanyName, o =>
            {
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item1 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    s.AprData.grupa.FirstOrDefault(x => x.id == "1006")?.podatak
                        .FirstOrDefault(p => p.naziv == "SkracenoPoslovnoIme")?.vrednost);
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item2 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    s.AprData.grupa.FirstOrDefault(x => x.id == "1046")?.podatak
                        .FirstOrDefault(p => p.naziv == "SkracenoPoslovnoIme")?.vrednost);
            })
            .ForMember(d => d.FirstName, o =>
            {
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item1 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    s.AprData.grupa.FirstOrDefault(x => x.id == "1027")?.podatak.FirstOrDefault(p => p.naziv == "Ime")
                        ?.vrednost);
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item2 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    {
                        var vrednost = s.AprData.grupa.FirstOrDefault(x => x.id == "1048")?.podatak
                            .FirstOrDefault(p => p.naziv == "ImeOsnivaca")
                            ?.vrednost;

                        return vrednost?[..(vrednost?.IndexOf(" ") ?? 0)];
                    });
            })
            .ForMember(d => d.LastName, o =>
            {
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item1 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    s.AprData.grupa.FirstOrDefault(x => x.id == "1027")?.podatak.FirstOrDefault(p => p.naziv == "Prezime")
                        ?.vrednost);
                
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item2 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                {
                    var vrednost = s.AprData.grupa.FirstOrDefault(x => x.id == "1048")?.podatak
                        .FirstOrDefault(p => p.naziv == "ImeOsnivaca")
                        ?.vrednost;

                    return vrednost?[(vrednost.IndexOf(" ", StringComparison.Ordinal) + 1)..];
                });
            })
            .ForMember(d => d.TaxNumber, o =>
            {
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item1 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    s.AprData.grupa.FirstOrDefault(x => x.id == "1017")?.podatak.FirstOrDefault(p => p.naziv == "PIB")
                        ?.vrednost);
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item2 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    s.AprData.grupa.FirstOrDefault(x => x.id == "1058")?.podatak.FirstOrDefault(p => p.naziv == "PIB")
                        ?.vrednost);
            })
            .ForMember(d => d.Email, o =>
            {
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item1 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    s.AprData.grupa.FirstOrDefault(x => x.id == "1013")?.podatak.FirstOrDefault(p => p.naziv == "EMailAdresa")
                        ?.vrednost);
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item2 && s.AprData.grupa.Any());
                o.MapFrom((s, _) =>
                    s.AprData.grupa.FirstOrDefault(x => x.id == "1054")?.podatak.FirstOrDefault(p => p.naziv == "EMailAdresa")
                        ?.vrednost);
            })
            .ForMember(d => d.TheAddress, o =>
            {
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item1 && s.AprData.grupa.Any());
                o.MapFrom((s, _) => s.AprData.grupa.FirstOrDefault(x => x.id == "1011"));
                
                o.Condition(s => s.Tip == PrivredniSubjekatMaticniBrojTip.Item2 && s.AprData.grupa.Any());
                o.MapFrom((s, _) => s.AprData.grupa.FirstOrDefault(x => x.id == "1052"));
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