using System;
using System.Collections.Generic;
using System.Text;
using rOS.Core.Api;

namespace rOS.eCommerce.Api.Production;

public interface IUnitMeasure : INamed
{
     string UnitCode { get; }
}
