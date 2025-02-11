using rOS.Core.Api;
using rOS.Core.Api.Business;

namespace rOS.Core.Entity;

public abstract class BusinessEntity : EntityBase, IBusinessEntity, INamed
{
    public abstract string Description { get; set; } 

    public abstract string Name { get; set; } 


    public BusinessEntity()
    { }
  


}
