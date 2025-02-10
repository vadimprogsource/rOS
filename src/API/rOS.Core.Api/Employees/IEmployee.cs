using rOS.Core.Api;
using rOS.Core.Api.Banking;
using rOS.Core.Api.Business;
using rOS.Core.Api.Orgs;
using rOS.Core.Api.Personalize;

namespace rOS.Core.Api.Employees;

public interface IEmployee : IPerson,IBusinessEntity
{
    IBusinessUnit Dept { get; }
    string Position { get; }
    IBankAccount Account { get; }

}
