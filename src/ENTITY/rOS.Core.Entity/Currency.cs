using rOS.Core.Api;


namespace rOS.Core.Entity;

public abstract class Currency : ICurrency
{
    public abstract string IsoCode { get; set; }

    public abstract string Name { get; set; } 

    public Currency() { }
   

}
