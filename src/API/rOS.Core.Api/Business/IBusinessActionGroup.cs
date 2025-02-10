using System;
namespace rOS.Core.Api.Business;

public interface IBusinessActionGroup : IBusinessAction
{
    IBusinessAction[] Actions { get; }
}

