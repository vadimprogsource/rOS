using System;
using rOS.Core.Api.Business;

namespace rOS.Core.Api.Accounting;

public interface IAccountScript
{
    IEnumerable<IAccountTransaction> Execute(IBusinessTransaction transaction);
}


public interface IAccountScriptFactory
{
    IAccountScript CreateScript(IBusinessAction action);
}

