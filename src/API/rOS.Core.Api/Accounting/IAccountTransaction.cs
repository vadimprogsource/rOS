using System;
using rOS.Core.Api.Business;

namespace rOS.Core.Api.Accounting;

public interface IAccountTransaction
{
    DateTime DateTimeOn { get; }
    IBusinessAction Action { get; }
    IAccount Debit { get; }
    IAccount Credit { get; }
    IDocument Document { get; }
    ICurrency Currency { get; }
    decimal Amount { get; }
}

