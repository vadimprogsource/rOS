using System;
namespace rOS.Core.Api.Orgs;

public interface ILegalEntity : IBusinessUnit
{
    string TaxCode { get; }
    string RegCode { get; }
}

