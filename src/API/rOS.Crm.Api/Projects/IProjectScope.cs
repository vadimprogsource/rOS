using System;
using rOS.Core.Api;
using rOS.Core.Api.Business;

namespace rOS.Crm.Api.Projects;

public interface IProjectScope :IBusinessEntity, IDocument,INamed ,ICoded,IDescription
{

}

