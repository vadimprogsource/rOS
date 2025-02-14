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

    IChangeContext<TEntity> GetChangeSet(TModel model);

}

