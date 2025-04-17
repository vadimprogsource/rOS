using System;
namespace rOS.Core.Api.Wallet
{
    public interface IWallet : IEntity, IDocument
    {
        decimal Balance { get; }
        ICurrency Currency { get; }
      
    }
}

