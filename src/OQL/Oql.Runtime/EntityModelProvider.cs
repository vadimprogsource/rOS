using System;
using System.Linq.Async;
using System.Reflection;
using Oql.Api.Runtime;

namespace Oql.Runtime;

public class EntityModelProvider<TEntity,TModel> : EntityModelBuilder,IEntityModelProvider<TEntity>  , IEntityModelProvider<TEntity,TModel>
{
    private readonly Func<TEntity, TModel>   _to_model;
    private readonly Action<TModel, TEntity> _from_model;
    private readonly Func<TModel, Guid>      _primary_key;


    public EntityModelProvider(IEnumerable<IEntityModelProperty>? properties = null)
    {

        if (properties == null || !properties.Any())
        {
            properties = Generate<TEntity, TModel>();
        }

        _to_model    = MakeConvertToModel<TEntity, TModel>(properties.Where(x=>x.Model.CanWrite && x.Entity.CanRead));
        _from_model  = MakeConvertFromModel<TModel, TEntity>(properties.Where(x =>x.Model.CanRead && x.Entity.CanWrite && !x.IsIdentity));

        IEntityModelProperty? identity = properties.FirstOrDefault(x => x.IsIdentity);

        if (identity != null)
        {
            _primary_key = MakeGetPrimaryKey<TModel, Guid>(identity);
            return;
        }


        _primary_key = x => Guid.Empty;

    }



    public Guid GetPrimaryKey(TModel model) => _primary_key(model);
    

    public TEntity FromModel(TModel model, TEntity entity)
    {
        _from_model(model, entity);
        return entity;
    }

    public TModel ToModel(TEntity entity) => _to_model(entity);

    public IEnumerable<TModel> ToModel(IEnumerable<TEntity> entity) => entity.Select(x => _to_model(x));
 

    public IAsyncEnumerable<TModel> ToModelAsync(IAsyncEnumerable<TEntity> entity) => entity.Select(x => _to_model(x));


    private readonly struct model_change_set : IChangeContext<TEntity>
    {
        private readonly TModel _model;
        private readonly Func<TModel, Guid> _getter;
        private readonly Action<TModel, TEntity> _setter;

        public model_change_set(TModel model, Func<TModel, Guid> getter, Action<TModel, TEntity> setter)
        {
            _model = model;
            _getter = getter;
            _setter = setter;
        }

        public Guid PrimaryKey => _getter(_model);

        public TEntity Change(TEntity entity)
        {
            _setter(_model, entity);
            return entity;
        }
    }


    public IChangeContext<TEntity> GetChangeSet(TModel model) => new model_change_set(model, _primary_key, _from_model);

    object IEntityModelProvider<TEntity>.ToModel(TEntity entity) => _to_model(entity) ?? throw new NullReferenceException() ;

    IEnumerable<object> IEntityModelProvider<TEntity>.ToModel(IEnumerable<TEntity> entity) => ToModel(entity).OfType<object>();

    IAsyncEnumerable<object> IEntityModelProvider<TEntity>.ToModelAsync(IAsyncEnumerable<TEntity> entity) => ToModelAsync(entity).OfType<TModel,object>();

    IChangeContext<TEntity> IEntityModelProvider<TEntity>.GetChangeSet(object model) => GetChangeSet((TModel)model);
    
}

