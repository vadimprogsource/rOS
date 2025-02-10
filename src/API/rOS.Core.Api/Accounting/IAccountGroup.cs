using System;
namespace rOS.Core.Api.Accounting;

public interface IAccountGroup : IEntity , ICoded , INamed , ITitled
{
    int Level { get; }
    IAccount[] Accounts { get; } 
}

