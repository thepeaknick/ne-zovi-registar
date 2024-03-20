namespace NeZoviReg.Abstractions.Shared.Model.Infrastructure;

public sealed class AprBusinessEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string CompanyName { get; set; }

    public Address TheAddress { get; set; }

    public string RegNumber { get; set; }

    public string TaxNumber { get; set; }

    public string Address=>  $"{TheAddress.Street} {TheAddress.StreetNumber}";
    
}

public sealed class Address
{
    public string Street { get; set; }
    
    public string StreetNumber { get; set; }
    
    public string FloorNumber { get; set; }
    
    public string ApartmentNumber { get; set; }
}