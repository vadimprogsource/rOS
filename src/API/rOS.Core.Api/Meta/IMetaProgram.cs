using System;
namespace rOS.Core.Api.Meta;

public interface IMetaProgram
{
    IMetaCategory[] Categories { get; }
    
}

