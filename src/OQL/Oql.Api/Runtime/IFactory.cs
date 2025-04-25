using System;
namespace Oql.Api.Runtime;


public interface IFactory<TEntity>
{
    Task<TEntity> CreateInstance();
}


public interface IFactory
{
    TEntity CreateInstance<TEntity>();
}
