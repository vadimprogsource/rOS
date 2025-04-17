using System;
using rOS.Core.Api.Banking;

namespace rOS.Core.Api.Wallet
{
    public interface IWalletTransaction : IEntity , IDocument
    {
        DateTime TransactionDateTime { get; }
        IWallet FromWallet { get; }
        IWallet ToWallet { get; }
        decimal Amount { get; }
        ICurrency Currency { get; }
    }
}

