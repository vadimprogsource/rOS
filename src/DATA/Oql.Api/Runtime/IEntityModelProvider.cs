using System;
namespace Oql.Api.Runtime;

public interface IChangeContext<TEntity>
{
    Guid PrimaryKey { get; }
    TEntity Change(TEntity entity);
}



public interface IEntityModelProvider<TEntity, TModel>
{
    Guid GetPrimaryKey(TModel model);
    TEntity FromModel(TModel model, TEntity entity);
    TModel  ToModel(TEntity entity);
    IEnumerable<TModel> ToModel(IEnumerable<TEntity> entity);
    IAsyncEnumerable<TModel> ToModelAsync(IAsyncEnumerable<TEntity> entity);
    IChangeContext<TEntity> GetChangeSet(TModel model);

}


public interface IEntityModelProvider<TEntity> 
{
    object ToModel(TEntity entity);
    IEnumerable<object> ToModel(IEnumerable<TEntity> entity);
    IAsyncEnumerable<object> ToModelAsync(IAsyncEnumerable<TEntity> entity);
    IChangeContext<TEntity> GetChangeSet(object model);
}

