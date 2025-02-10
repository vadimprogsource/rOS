using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Core.Api.Business;

public interface IBusinessEntity : IEntity , INamed
{
    string Description { get; }
}
