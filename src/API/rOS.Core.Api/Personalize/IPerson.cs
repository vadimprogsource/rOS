using rOS.Core.Api.Banking;
using rOS.Core.Api.Business;

namespace rOS.Core.Api.Personalize;

public interface IPerson :IBusinessEntity  
{


    string Weight { get; set; }
    string Height { get; set; }

    string FootSize { get; set; }


    IAddress Address { get; }
    IPassport Passport { get; }

    IBankAccount[] Accounts { get; }
}
