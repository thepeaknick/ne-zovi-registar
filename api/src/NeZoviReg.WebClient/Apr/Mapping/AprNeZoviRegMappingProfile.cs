using AutoMapper;
using NeZoviReg.Abstractions.Shared.Model.Infrastructure;
using NeZoviReg.WebClient.PlService;

namespace NeZoviReg.WebClient.Apr.Mapping;

public class AprNeZoviRegMappingProfile : Profile
{
    public AprNeZoviRegMappingProfile()
    {
        CreateMap<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity>()
            .ForMember(d => d.RegNumber, o => { o.MapFrom<RegNumberResolver>(); })
            .ForMember(d => d.CompanyName, o => { o.MapFrom<CompanyNameResolver>(); })
            .ForMember(d => d.FirstName, o => { o.MapFrom<FirstNameResolver>(); })
            .ForMember(d => d.LastName, o => { o.MapFrom<LastNameResolver>(); })
            .ForMember(d => d.TaxNumber, o => { o.MapFrom<TaxNumberResolver>(); })
            .ForMember(d => d.Email, o => { o.MapFrom<EmailResolver>(); })
            .ForMember(d => d.TheAddress, o => { o.MapFrom<AddressResolver>(); });

        CreateMap<(Grupa Grp, PrivredniSubjekatMaticniBrojTip Tip), Address>()
            .ConstructUsing(x => new Address())
            .ForMember(s => s.Street, o => o.MapFrom<AddressStreetResolver>())
            .ForMember(s => s.StreetNumber, o => o.MapFrom<AddressStreetNumberResolver>())
            .ForMember(s => s.FloorNumber, o => o.MapFrom<AddressStreetFloorNumberResolver>())
            .ForMember(s => s.ApartmentNumber, o => o.MapFrom<AddressApartmentNumberResolver>());
    }
}

internal class RegNumberResolver : IValueResolver<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip),
    AprBusinessEntity, string>
{
    public string Resolve((PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip) s,
        AprBusinessEntity d, string member, ResolutionContext context)
    {
        return s.Tip switch
        {
            PrivredniSubjekatMaticniBrojTip.Item1 when s.AprData.grupa.Any() => s.AprData.grupa //DOO
                .FirstOrDefault(x => x.id == "1001")
                ?.podatak.FirstOrDefault(p => p.naziv == "MaticniBroj")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item2 when s.AprData.grupa.Any() => s.AprData.grupa //Preduzetnik
                .FirstOrDefault(x => x.id == "1041")
                ?.podatak.FirstOrDefault(p => p.naziv == "MaticniBroj")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item3 when s.AprData.grupa.Any() => s.AprData.grupa //Udruzenje
                .FirstOrDefault(x => x.id == "55")
                ?.podatak.FirstOrDefault(p => p.naziv == "MaticniBroj")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item4 when s.AprData.grupa.Any() => s.AprData.grupa //Stec. masa
                .FirstOrDefault(x => x.id == "1076")
                ?.podatak.FirstOrDefault(p => p.naziv == "MaticniBroj")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item5 when s.AprData.grupa.Any() => s.AprData.grupa //Fondacija
                .FirstOrDefault(x => x.id == "78")
                ?.podatak.FirstOrDefault(p => p.naziv == "MaticniBroj")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item6 when s.AprData.grupa.Any() => s.AprData.grupa //Sports. udruzenje
                .FirstOrDefault(x => x.id == "108")
                ?.podatak.FirstOrDefault(p => p.naziv == "MaticniBroj")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item7 when s.AprData.grupa.Any() => s.AprData.grupa //Komora
                .FirstOrDefault(x => x.id == "2001")
                ?.podatak.FirstOrDefault(p => p.naziv == "MaticniBroj")
                ?.vrednost ?? string.Empty,
            _ => string.Empty
        };
    }
}

internal class CompanyNameResolver : IValueResolver<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip),
    AprBusinessEntity, string>
{
    public string Resolve((PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip) s,
        AprBusinessEntity d, string member, ResolutionContext context)
    {
        switch (s.Tip)
        {
            case PrivredniSubjekatMaticniBrojTip.Item1 when s.AprData.grupa.Any(): //DOO
            {
               var name = s.AprData.grupa.FirstOrDefault(x => x.id == "1006")?.podatak.FirstOrDefault(p => p.naziv == "SkracenoPoslovnoIme")?.vrednost ??
                           string.Empty;
                if (string.IsNullOrEmpty(name))
                {
                    name = s.AprData.grupa.FirstOrDefault(x => x.id == "1007")?.podatak.FirstOrDefault(p => p.naziv == "Naziv")?.vrednost ?? string.Empty;
                }
                if (string.IsNullOrEmpty(name))
                {
                    name = s.AprData.grupa.FirstOrDefault(x => x.id == "1005")?.podatak.FirstOrDefault(p => p.naziv == "PoslovnoIme")?.vrednost ?? string.Empty;
                }

                return name.Trim('"');
            }
            case PrivredniSubjekatMaticniBrojTip.Item2 when s.AprData.grupa.Any(): //Preduzetnik
            {
                var name = s.AprData.grupa.FirstOrDefault(x => x.id == "1046")?.podatak.FirstOrDefault(p => p.naziv == "SkracenoPoslovnoIme")?.vrednost ??
                           string.Empty;
                if (string.IsNullOrEmpty(name))
                {
                    name = s.AprData.grupa.FirstOrDefault(x => x.id == "1047")?.podatak.FirstOrDefault(p => p.naziv == "Naziv")?.vrednost ?? string.Empty;
                }
                if (string.IsNullOrEmpty(name))
                {
                    name = s.AprData.grupa.FirstOrDefault(x => x.id == "1045")?.podatak.FirstOrDefault(p => p.naziv == "PoslovnoIme")?.vrednost ?? string.Empty;
                }

                return name.Trim('"');
            }
            case PrivredniSubjekatMaticniBrojTip.Item3 when s.AprData.grupa.Any(): //Udruzenje
            {
                var name = s.AprData.grupa.FirstOrDefault(x => x.id == "50")?.podatak.FirstOrDefault(p => p.naziv == "SkracenoPoslovnoIme")?.vrednost ??
                           string.Empty;
                if (string.IsNullOrEmpty(name))
                {
                    name = s.AprData.grupa.FirstOrDefault(x => x.id == "49")?.podatak.FirstOrDefault(p => p.naziv == "Naziv")?.vrednost ?? string.Empty;
                }
                
                return name.Trim('"');
            }
            case PrivredniSubjekatMaticniBrojTip.Item4 when s.AprData.grupa.Any(): //Stec. masa
            {
                return s.AprData.grupa.FirstOrDefault(x => x.id == "1080")?.podatak.FirstOrDefault(p => p.naziv == "Naziv")?.vrednost.Trim('"') ?? string.Empty;
            }
            case PrivredniSubjekatMaticniBrojTip.Item5 when s.AprData.grupa.Any(): //Fondacija
            {
                var name = s.AprData.grupa.FirstOrDefault(x => x.id == "73")?.podatak.FirstOrDefault(p => p.naziv == "SkraceniNaziv")?.vrednost ?? string.Empty;
                if (string.IsNullOrEmpty(name))
                {
                    name = s.AprData.grupa.FirstOrDefault(x => x.id == "72")?.podatak.FirstOrDefault(p => p.naziv == "Naziv")?.vrednost ?? string.Empty;
                }
                if (string.IsNullOrEmpty(name))
                {
                    name = s.AprData.grupa.FirstOrDefault(x => x.id == "74")?.podatak.FirstOrDefault(p => p.naziv == "PoslovnoImeNaStranomJeziku")?.vrednost ?? string.Empty;
                }

                return name.Trim('"');
            }
            case PrivredniSubjekatMaticniBrojTip.Item6 when s.AprData.grupa.Any(): //Sports. udruzenja
            {
                var name = s.AprData.grupa.FirstOrDefault(x => x.id == "103")?.podatak.FirstOrDefault(p => p.naziv == "SkracenoPoslovnoIme")?.vrednost ??
                           string.Empty;
                if (string.IsNullOrEmpty(name))
                {
                    name = s.AprData.grupa.FirstOrDefault(x => x.id == "102")?.podatak.FirstOrDefault(p => p.naziv == "PunNaziv")?.vrednost ?? string.Empty;
                }
                
                return name.Trim('"');
            }
            case PrivredniSubjekatMaticniBrojTip.Item7 when s.AprData.grupa.Any(): //Komora
            {
                var name = s.AprData.grupa.FirstOrDefault(x => x.id == "2005")?.podatak.FirstOrDefault(p => p.naziv == "SkraceniNaziv")?.vrednost ??
                           string.Empty;
                if (string.IsNullOrEmpty(name))
                {
                    name = s.AprData.grupa.FirstOrDefault(x => x.id == "2004")?.podatak.FirstOrDefault(p => p.naziv == "PoslovnoIme")?.vrednost ?? string.Empty;
                }

                return name.Trim('"');
            }
            default:
                return string.Empty;
        }

        ;
    }
}

internal class FirstNameResolver : IValueResolver<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip),
    AprBusinessEntity, string>
{
    public string Resolve((PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip) s,
        AprBusinessEntity d, string member, ResolutionContext context)
    {
        switch (s.Tip)
        {
            case PrivredniSubjekatMaticniBrojTip.Item1 when s.AprData.grupa.Any(): //DOO
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "1027")
                    ?.podatak.FirstOrDefault(p => p.naziv == "Ime")
                    ?.vrednost ?? string.Empty;
            case PrivredniSubjekatMaticniBrojTip.Item2 when s.AprData.grupa.Any(): //Preduzetnik
            {
                var vrednost = s.AprData.grupa.FirstOrDefault(x => x.id == "1048")?.podatak
                    .FirstOrDefault(p => p.naziv == "ImeOsnivaca")
                    ?.vrednost;

                return vrednost?[..(vrednost?.IndexOf(" ") ?? 0)] ?? string.Empty;
            }
            case PrivredniSubjekatMaticniBrojTip.Item3 when s.AprData.grupa.Any(): //Udruzenje
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "66")
                    ?.podatak.FirstOrDefault(p => p.naziv == "ImeOsobe")
                    ?.vrednost ?? string.Empty;
            case PrivredniSubjekatMaticniBrojTip.Item4 when s.AprData.grupa.Any(): //Stec. masa
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "1081")
                    ?.podatak.FirstOrDefault(p => p.naziv == "ImeOsobeCirilica")
                    ?.vrednost ?? string.Empty;
            case PrivredniSubjekatMaticniBrojTip.Item5 when s.AprData.grupa.Any(): //Fondacija
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "88")
                    ?.podatak.FirstOrDefault(p => p.naziv == "ImeOsobeCirilica")
                    ?.vrednost ?? string.Empty;
            case PrivredniSubjekatMaticniBrojTip.Item6 when s.AprData.grupa.Any(): //Sports. udruzenja
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "119")
                    ?.podatak.FirstOrDefault(p => p.naziv == "ImeOsobeCirilica")
                    ?.vrednost ?? string.Empty;
            case PrivredniSubjekatMaticniBrojTip.Item7 when s.AprData.grupa.Any(): //Komora
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "2017")
                    ?.podatak.FirstOrDefault(p => p.naziv == "Ime")
                    ?.vrednost ?? string.Empty;
            default:
                return string.Empty;
        }
    }
}

internal class LastNameResolver : IValueResolver<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip),
    AprBusinessEntity, string>
{
    public string Resolve((PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip) s,
        AprBusinessEntity d, string member, ResolutionContext context)
    {
        switch (s.Tip)
        {
            case PrivredniSubjekatMaticniBrojTip.Item1 when s.AprData.grupa.Any(): //DOO
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "1027")
                    ?.podatak.FirstOrDefault(p => p.naziv == "Prezime")
                    ?.vrednost ?? string.Empty;
            case PrivredniSubjekatMaticniBrojTip.Item2 when s.AprData.grupa.Any(): //Preduzetnik
            {
                var vrednost = s.AprData.grupa.FirstOrDefault(x => x.id == "1048")?.podatak
                    .FirstOrDefault(p => p.naziv == "ImeOsnivaca")
                    ?.vrednost;

                return vrednost?[(vrednost.IndexOf(" ", StringComparison.Ordinal) + 1)..] ?? string.Empty;
            }
            case PrivredniSubjekatMaticniBrojTip.Item3 when s.AprData.grupa.Any(): //Udruzenje
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "66")
                    ?.podatak.FirstOrDefault(p => p.naziv == "PrezimeOsobe")
                    ?.vrednost ?? string.Empty;
            case PrivredniSubjekatMaticniBrojTip.Item4 when s.AprData.grupa.Any(): //Stec. masa
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "1081")
                    ?.podatak.FirstOrDefault(p => p.naziv == "PrezimeOsobeCirilica")
                    ?.vrednost ?? string.Empty;
            case PrivredniSubjekatMaticniBrojTip.Item5 when s.AprData.grupa.Any(): //Fondacija
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "88")
                    ?.podatak.FirstOrDefault(p => p.naziv == "PrezimeOsobeCirilica")
                    ?.vrednost ?? string.Empty;
            case PrivredniSubjekatMaticniBrojTip.Item6 when s.AprData.grupa.Any(): //Sports. udruzenja
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "119")
                    ?.podatak.FirstOrDefault(p => p.naziv == "PrezimeOsobeCirilica")
                    ?.vrednost ?? string.Empty;
            case PrivredniSubjekatMaticniBrojTip.Item7 when s.AprData.grupa.Any(): //Komora
                return s.AprData.grupa
                    .FirstOrDefault(x => x.id == "2017")
                    ?.podatak.FirstOrDefault(p => p.naziv == "Prezime")
                    ?.vrednost ?? string.Empty;
            default:
                return string.Empty;
        }
    }
}

internal class TaxNumberResolver : IValueResolver<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip),
    AprBusinessEntity, string>
{
    public string Resolve((PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip) s,
        AprBusinessEntity d, string member, ResolutionContext context)
    {
        return s.Tip switch
        {
            PrivredniSubjekatMaticniBrojTip.Item1 when s.AprData.grupa.Any() => s.AprData.grupa //DOO
                .FirstOrDefault(x => x.id == "1017")
                ?.podatak.FirstOrDefault(p => p.naziv == "PIB")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item2 when s.AprData.grupa.Any() => s.AprData.grupa //Preduzetnik
                .FirstOrDefault(x => x.id == "1058")
                ?.podatak.FirstOrDefault(p => p.naziv == "PIB")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item3 when s.AprData.grupa.Any() => s.AprData.grupa //Udruzenje
                .FirstOrDefault(x => x.id == "56")
                ?.podatak.FirstOrDefault(p => p.naziv == "PIB")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item4 when s.AprData.grupa.Any() => s.AprData.grupa //Stec. masa
                .FirstOrDefault(x => x.id == "1077")
                ?.podatak.FirstOrDefault(p => p.naziv == "PIB")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item5 when s.AprData.grupa.Any() => s.AprData.grupa //Fondacija
                .FirstOrDefault(x => x.id == "79")
                ?.podatak.FirstOrDefault(p => p.naziv == "PIB")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item6 when s.AprData.grupa.Any() => s.AprData.grupa //Sports. udruzenja
                .FirstOrDefault(x => x.id == "109")
                ?.podatak.FirstOrDefault(p => p.naziv == "PIB")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item7 when s.AprData.grupa.Any() => s.AprData.grupa //Komora
                .FirstOrDefault(x => x.id == "2003")
                ?.podatak.FirstOrDefault(p => p.naziv == "PIB")
                ?.vrednost ?? string.Empty,
            _ => string.Empty
        };
    }
}

internal class EmailResolver : IValueResolver<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip),
    AprBusinessEntity, string>
{
    public string Resolve((PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip) s,
        AprBusinessEntity d, string member, ResolutionContext context)
    {
        return s.Tip switch
        {
            PrivredniSubjekatMaticniBrojTip.Item1 when s.AprData.grupa.Any() => s.AprData.grupa //DOO
                .FirstOrDefault(x => x.id == "1013")
                ?.podatak.FirstOrDefault(p => p.naziv == "EMailAdresa")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item2 when s.AprData.grupa.Any() => s.AprData.grupa //Preduzetnik
                .FirstOrDefault(x => x.id == "1054")
                ?.podatak.FirstOrDefault(p => p.naziv == "EMailAdresa")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item3 when s.AprData.grupa.Any() => s.AprData.grupa //Udruzenje
                .FirstOrDefault(x => x.id == "69")
                ?.podatak.FirstOrDefault(p => p.naziv == "EPosta")
                ?.vrednost ?? string.Empty,
            //PrivredniSubjekatMaticniBrojTip.Item4 nema e postu
            PrivredniSubjekatMaticniBrojTip.Item5 when s.AprData.grupa.Any() => s.AprData.grupa //Fondacija
                .FirstOrDefault(x => x.id == "95")
                ?.podatak.FirstOrDefault(p => p.naziv == "EPosta")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item6 when s.AprData.grupa.Any() => s.AprData.grupa //Sport. udruzenje
                .FirstOrDefault(x => x.id == "122")
                ?.podatak.FirstOrDefault(p => p.naziv == "EPosta")
                ?.vrednost ?? string.Empty,
            PrivredniSubjekatMaticniBrojTip.Item7 when s.AprData.grupa.Any() => s.AprData.grupa //Komora
                .FirstOrDefault(x => x.id == "2020")
                ?.podatak.FirstOrDefault(p => p.naziv == "EPosta")
                ?.vrednost ?? string.Empty,
            _ => string.Empty
        };
    }
}

internal class AddressResolver : IValueResolver<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip),
    AprBusinessEntity, Address>
{
    public Address Resolve((PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip) s,
        AprBusinessEntity d, Address member, ResolutionContext context)
    {
        switch (s.Tip)
        {
            case PrivredniSubjekatMaticniBrojTip.Item1 when s.AprData.grupa.Any(): //DOO
            {
                var grupa = s.AprData.grupa.FirstOrDefault(x => x.id == "1011");
                return context.Mapper.Map<Address>((grupa, s.Tip));
            }
            case PrivredniSubjekatMaticniBrojTip.Item2 when s.AprData.grupa.Any(): //Preduzetnik
            {
                var grupa = s.AprData.grupa.FirstOrDefault(x => x.id == "1052");
                return context.Mapper.Map<Address>((grupa, s.Tip));
            }
            case PrivredniSubjekatMaticniBrojTip.Item3 when s.AprData.grupa.Any(): //Udruzenje
            {
                var grupa = s.AprData.grupa.FirstOrDefault(x => x.id == "54");
                return context.Mapper.Map<Address>((grupa, s.Tip));
            }
            case PrivredniSubjekatMaticniBrojTip.Item4 when s.AprData.grupa.Any(): //Stec. masa
            {
                var grupa = s.AprData.grupa.FirstOrDefault(x => x.id == "1080");
                return context.Mapper.Map<Address>((grupa, s.Tip));
            }
            case PrivredniSubjekatMaticniBrojTip.Item5 when s.AprData.grupa.Any(): //Fondacija
            {
                var grupa = s.AprData.grupa.FirstOrDefault(x => x.id == "77");
                return context.Mapper.Map<Address>((grupa, s.Tip));
            }
            case PrivredniSubjekatMaticniBrojTip.Item6 when s.AprData.grupa.Any(): //Sport. udruzenje
            {
                var grupa = s.AprData.grupa.FirstOrDefault(x => x.id == "107");
                return context.Mapper.Map<Address>((grupa, s.Tip));
            }
            case PrivredniSubjekatMaticniBrojTip.Item7 when s.AprData.grupa.Any(): //Komora
            {
                var grupa = s.AprData.grupa.FirstOrDefault(x => x.id == "2007");
                return context.Mapper.Map<Address>((grupa, s.Tip));
            }
            default:
                return default!;
        }
    }
}

internal class AddressStreetResolver : IValueResolver<(Grupa AprGrupa, PrivredniSubjekatMaticniBrojTip Tip),
    Address, string>
{
    public string Resolve((Grupa AprGrupa, PrivredniSubjekatMaticniBrojTip Tip) s,
        Address d, string member, ResolutionContext context)
    {
        switch (s.Tip)
        {
            case PrivredniSubjekatMaticniBrojTip.Item3: //Udruzenje
            case PrivredniSubjekatMaticniBrojTip.Item5: //Fondacija
            case PrivredniSubjekatMaticniBrojTip.Item6: //Sports. udruzenje
            {
                return s.AprGrupa?.podatak?.FirstOrDefault(p => p.naziv == "NazivUliceCirilicni")?.vrednost ??
                       string.Empty;
            }
            case PrivredniSubjekatMaticniBrojTip.Item7: //Komora
            {
                return s.AprGrupa?.podatak?.FirstOrDefault(p => p.naziv == "UlicaNazivCirilica")?.vrednost ??
                       string.Empty;
            }
            case PrivredniSubjekatMaticniBrojTip.Item4: //Stec. masa
            {
                return s.AprGrupa?.podatak?.FirstOrDefault(p => p.naziv == "UlicaNazivCir")?.vrednost ?? string.Empty;
            }

            default:
                return s.AprGrupa?.podatak?.FirstOrDefault(p => p.naziv == "NazivUlice")?.vrednost ?? string.Empty;
                ;
        }
    }
}

internal class AddressStreetNumberResolver : IValueResolver<(Grupa AprGrupa, PrivredniSubjekatMaticniBrojTip Tip),
    Address, string>
{
    public string Resolve((Grupa AprGrupa, PrivredniSubjekatMaticniBrojTip Tip) s,
        Address d, string member, ResolutionContext context)
    {
        switch (s.Tip)
        {
            case PrivredniSubjekatMaticniBrojTip.Item4: //Stec. masa
            {
                return s.AprGrupa?.podatak?.FirstOrDefault(p => p.naziv == "UlicaBroj")?.vrednost ?? string.Empty;
            }

            default:
                return s.AprGrupa?.podatak?.FirstOrDefault(p => p.naziv == "AdresaBroj")?.vrednost ?? string.Empty;
                ;
        }
    }
}

internal class AddressStreetFloorNumberResolver : IValueResolver<(Grupa AprGrupa, PrivredniSubjekatMaticniBrojTip Tip),
    Address, string>
{
    public string Resolve((Grupa AprGrupa, PrivredniSubjekatMaticniBrojTip Tip) s,
        Address d, string member, ResolutionContext context)
    {
        switch (s.Tip)
        {
            case PrivredniSubjekatMaticniBrojTip.Item4: //Stec. masa
            {
                return s.AprGrupa?.podatak?.FirstOrDefault(p => p.naziv == "UlicaSprat")?.vrednost ?? string.Empty;
            }

            default:
                return s.AprGrupa?.podatak?.FirstOrDefault(p => p.naziv == "AdresaSprat")?.vrednost ?? string.Empty;
                ;
        }
    }
}

internal class AddressApartmentNumberResolver : IValueResolver<(Grupa AprGrupa, PrivredniSubjekatMaticniBrojTip Tip),
    Address, string>
{
    public string Resolve((Grupa AprGrupa, PrivredniSubjekatMaticniBrojTip Tip) s,
        Address d, string member, ResolutionContext context)
    {
        switch (s.Tip)
        {
            case PrivredniSubjekatMaticniBrojTip.Item4: //Stec. masa
            {
                return s.AprGrupa?.podatak?.FirstOrDefault(p => p.naziv == "UlicaStanBroj")?.vrednost ?? string.Empty;
            }

            default:
                return s.AprGrupa?.podatak?.FirstOrDefault(p => p.naziv == "AdresaBrojStana")?.vrednost ?? string.Empty;
                ;
        }
    }
}