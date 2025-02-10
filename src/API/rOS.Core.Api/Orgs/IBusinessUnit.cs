using rOS.Core.Api;
using rOS.Core.Api.Business;
using rOS.Core.Api.Employees;

namespace rOS.Core.Api.Orgs;

public interface IBusinessUnit : IBusinessEntity , IContact
{
   IReadOnlyCollection<IEmployee> Employees { get; }
}
