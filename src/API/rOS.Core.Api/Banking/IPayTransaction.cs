using rOS.Core.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Core.Api.Banking;

public interface IPayTransaction : IEntity, IDocument 
{
    DateTime TransactionDateTime { get; }
    IBankAccount FromAccount { get; }
    IBankAccount ToAccount { get; }
    decimal Amount { get; }
    ICurrency Currency { get; }

}
