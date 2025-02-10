using System;
namespace rOS.Core.Api.Accounting;

public interface IAccountRelation
{
    IAccount Primary { get; }
    IAccount Secondary { get; }
    bool IsDebit { get; }
    bool IsCredit { get; }

}

