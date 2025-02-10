using System;
namespace rOS.Core.Api.Business;

public interface IBusinessTransaction : IEntity
{
    DateTime DateTimeOn { get; }
    IBusinessEntity FromEntity { get; }
    IBusinessEntity ToEntity { get; }
    IBusinessAction Action { get; }
    IDocument Document { get; }
    ICurrency Currency { get; }
    decimal Amount { get; }
    bool UseAccounting { get; }
}

