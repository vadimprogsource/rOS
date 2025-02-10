using System;
namespace rOS.Core.Api.Accounting;

public interface IAccountLog
{
    IAccount Account { get; }
    IAccountTransaction Transaction { get; }
    IAccountLog Chain { get; }
    ICurrency Currency { get; }
    decimal Debit { get; }
    decimal Credit { get; }
    decimal Balance { get; }
}

