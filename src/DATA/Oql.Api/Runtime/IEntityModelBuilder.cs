using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Oql.Api.Runtime;


public interface IEntityModelBuilder
{
    IEntityModelBuilder Use(PropertyInfo property,bool isIdentity=false);
    object Build();
}


public interface IEntityModelBuilder<TEntity>
{
    IEntityModelBuilder<TEntity> Use<TResult>(Expression<Func<TEntity, TResult>> property,bool isIdentity = false);
    IEntityModelProvider<TEntity> Build();
}

