using System;
using rOS.Core.Api.Accounting;

namespace rOS.Core.Api.Wallet
{

   

    public interface IWalletLog : IEntity , IDocument
    {
        IWallet Wallet { get; }
        IWalletTransaction Transaction { get; }
        IWalletLog Chain { get; }
        WalletOperation Operation { get; }
        ICurrency Currency { get; }
        decimal Amount { get; }
        decimal Balance { get; }
    }
}

