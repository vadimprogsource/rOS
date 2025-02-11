using rOS.Core.Api;

namespace rOS.Core.Entity;

public class Address : IAddress
{
    public string Country { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string Floor { get; set; } = string.Empty;

    public string House { get; set; } = string.Empty;

    public Address() { }

    public Address(IAddress addr) 
    {
        Country = addr.Country;
        Region = addr.Region;
        City = addr.City;
        Street = addr.Street;
        Floor = addr.Floor;
        House = addr.House;
    }

}
