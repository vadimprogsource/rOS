using rOS.Core.Api;
using rOS.Core.Api.Business;


namespace rOS.Core.Entity;

public abstract class ContactBase : BusinessEntity, IContact
{
    public abstract string Cellular { get; set; } 

    public abstract string Phone { get; set; } 
    public abstract string Email { get; set; } 
    public abstract string Title { get; set; } 

    public ContactBase() { }


}
