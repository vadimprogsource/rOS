using System;
namespace Oql.Api.Runtime;

public interface IInstanceProvider<TInterface,TEntity>
{
    TEntity GetInstance(TInterface ifc);
}

