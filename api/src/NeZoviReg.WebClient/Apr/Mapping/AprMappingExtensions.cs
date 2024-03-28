using System.Linq.Expressions;
using AutoMapper;
using NeZoviReg.Abstractions.Shared.Model.Infrastructure;
using NeZoviReg.WebClient.PlService;

namespace NeZoviReg.WebClient.Apr.Mapping;

public static class AprMappingExtensions
{
    public static IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> MapRegNumber(
            this IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> expr,
            Func<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
            string srcMemberIdentifier,
            string srcMemberValueIdentifier = "MaticniBroj")
    {
        return expr.MapConditional(d => d.RegNumber, cond, srcMemberIdentifier, srcMemberValueIdentifier);
    }
    
    public static IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> MapCompanyName(
        this IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> expr,
        Func<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
        string srcMemberIdentifier,
        string srcMemberValueIdentifier = "SkracenoPoslovnoIme")
    {
        return expr.MapConditional(d => d.CompanyName, cond, srcMemberIdentifier, srcMemberValueIdentifier);
    }
    
    public static IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> MapFirstName(
        this IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> expr,
        Func<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
        string srcMemberIdentifier,
        string srcMemberValueIdentifier = "Ime")
    {
        return expr.MapConditional(d => d.FirstName, cond, srcMemberIdentifier, srcMemberValueIdentifier);
    }
    
    public static IMappingExpression<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> MapImeOsnivaca(
        this IMappingExpression<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> expr,
        Func<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
        string srcMemberIdentifier,
        string srcMemberValueIdentifier = "ImeOsnivaca")
    {
        expr.ForMember(d=>d.FirstName,
            f =>
            {
                f.PreCondition(cond);
                f.MapFrom((src, _) =>
                {
                    
                    var vrednost = src.AprData.grupa.FirstOrDefault(x => x.id == srcMemberIdentifier)?.podatak
                        .FirstOrDefault(p => p.naziv == srcMemberValueIdentifier)
                        ?.vrednost;

                    var x=  vrednost?[..(vrednost?.IndexOf(" ") ?? 0)];

                    return x;
                });
            });

        return expr;
    }
    
    public static IMappingExpression<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> MapPrezimeOsnivaca(
        this IMappingExpression<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> expr,
        Func<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
        string srcMemberIdentifier,
        string srcMemberValueIdentifier = "ImeOsnivaca")
    {
        expr.ForMember(d=>d.FirstName,
            f =>
            {
                f.PreCondition(cond);
                f.MapFrom((src, _) =>
                {
                    
                    var vrednost = src.AprData.grupa.FirstOrDefault(x => x.id == srcMemberIdentifier)?.podatak
                        .FirstOrDefault(p => p.naziv == srcMemberValueIdentifier)
                        ?.vrednost;

                    var x=   vrednost?[(vrednost.IndexOf(" ", StringComparison.Ordinal) + 1)..];

                    return x;
                });
            });

        return expr;
    }
    
    public static IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> MapLastName(
        this IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> expr,
        Func<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
        string srcMemberIdentifier,
        string srcMemberValueIdentifier = "Prezime")
    {
        return expr.MapConditional(d => d.LastName, cond, srcMemberIdentifier, srcMemberValueIdentifier);
    }
    
    public static IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> MapTaxNumber(
        this IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> expr,
        Func<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
        string srcMemberIdentifier,
        string srcMemberValueIdentifier = "PIB")
    {
        return expr.MapConditional(d => d.TaxNumber, cond, srcMemberIdentifier, srcMemberValueIdentifier);
    }
    public static IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> MapEmail(
        this IMappingExpression<(PrivredniSubjekat, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> expr,
        Func<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
        string srcMemberIdentifier,
        string srcMemberValueIdentifier = "EMailAdresa")
    {
        return expr.MapConditional(d => d.Email, cond, srcMemberIdentifier, srcMemberValueIdentifier);
    }
    
    public static IMappingExpression<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> MapAddress(
        this IMappingExpression<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> expr,
        Func<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
        string srcMemberIdentifier)
    {
         expr.ForMember(d=>d.TheAddress,
            f =>
            {
                f.PreCondition(cond);
                f.MapFrom((src, _) => src.AprData.grupa.FirstOrDefault(x => x.id == srcMemberIdentifier));
            });

        return expr;
    }
    

    public static IMappingExpression<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> MapConditional(
            this IMappingExpression<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), AprBusinessEntity> expr,
            Expression<Func<AprBusinessEntity, string>> destinationMember,
            Func<(PrivredniSubjekat AprData, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
            string srcMemberIdentifier,
            string srcMemberValueIdentifier)
    {
        expr.ForMember(destinationMember,
            f =>
            {
                f.PreCondition(cond);
                f.MapFrom((src, _) => src.AprData.grupa.FirstOrDefault(x => x.id == srcMemberIdentifier)?.podatak
                    .FirstOrDefault(p => p.naziv == srcMemberValueIdentifier)?.vrednost);
            });

        return expr;
    }
    
    public static IMappingExpression<(Grupa Grp, PrivredniSubjekatMaticniBrojTip Tip), Address> MapConditional(
        this IMappingExpression<(Grupa Grp, PrivredniSubjekatMaticniBrojTip Tip), Address> expr,
        Expression<Func<Address, string>> destinationMember,
        Func<(Grupa Grp, PrivredniSubjekatMaticniBrojTip Tip), bool> cond,
        string srcMemberValueIdentifier)
    {
        expr.ForMember(destinationMember,
            f =>
            {
                f.PreCondition(cond);
                f.MapFrom((src, _) => src.Grp?.podatak.FirstOrDefault(p => p.naziv == srcMemberValueIdentifier)?.vrednost);
            });

        return expr;
    }
    
    public static IMappingExpression<Grupa, Address> Map(this IMappingExpression<Grupa, Address> expr,
        Expression<Func<Address, string>> destinationMember,
        string srcMemberValueIdentifier)
    {
        expr.ForMember(destinationMember,
            f =>
            {
                f.MapFrom((src, _) => src.podatak.FirstOrDefault(p => p.naziv == srcMemberValueIdentifier)?.vrednost);
            });

        return expr;
    }
}