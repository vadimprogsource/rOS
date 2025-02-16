using System;
using System.Linq.Async.Emit;
using System.Linq.Async.Reflection;
using System.Reflection;
using Oql.Api.Attributes;
using Oql.Api.Runtime;

namespace Oql.Runtime;



public class EntityModelBuilder
{
    public static string? ConvertToString<T>(T obj) => obj == null ? string.Empty : obj.ToString();

    public static TResult? ConvertTo<TSource, TResult>(TSource obj)
    {
        if (obj == null) return default;

        Type? resultType = Nullable.GetUnderlyingType(typeof(TResult));

        resultType ??= typeof(TResult);
        
        if (resultType.IsEnum)
        {
            resultType = Enum.GetUnderlyingType(resultType);
        }
  
        return (TResult)Convert.ChangeType(obj, resultType);
    }

    public static T ConvertToEnum<T>(object obj)
    {
        return (T)Enum.ToObject(typeof(T), obj);
    }

    public static bool IsValid<T>(T value) => value != null;

    private readonly static IMethod s_convert_to_string = new Method<EntityModelBuilder>(x => ConvertToString(1));
    private readonly static IMethod s_convert_to_enum   = new Method<EntityModelBuilder>(x => ConvertToEnum<int>(1));
    private readonly static IMethod s_convert_to        = new Method<EntityModelBuilder>(x => ConvertTo<int, string>(int.MinValue));
    private readonly static IMethod s_is_valid          = new Method<EntityModelBuilder>(x => IsValid(int.MinValue));





    internal static ICodeBuilder EmitCallConvert(ICodeBuilder codeBuilder, Type sourceType, Type targetType)
    {
        if (sourceType == targetType)
        {
            return codeBuilder;
        }

        if (targetType == typeof(string))
        {
            return codeBuilder.Call(s_convert_to_string,targetType);
        }

        if (targetType.IsEnum)
        {
            return codeBuilder.Call(s_convert_to_enum, targetType);
        }

        return codeBuilder.Call(s_convert_to,sourceType, targetType);
    }


    internal static Func<TEntity, TModel> MakeConvertToModel<TEntity, TModel>(IEnumerable<IEntityModelProperty> properties)
    {
        MethodCodeBuilder<TEntity, TModel> codeBuilder = new(typeof(EntityModelBuilder));
        codeBuilder.NewObject<TModel>();

        foreach (IEntityModelProperty prop in properties)
        {
            EmitCallConvert(codeBuilder.This().Argument().GetValue(prop.Entity), prop.Entity.PropertyType, prop.Model.PropertyType).SetValue(prop.Model);
        }

        return codeBuilder.BuildFunc();
    }


    internal static Func<TModel, TPrimaryKey> MakeGetPrimaryKey<TModel, TPrimaryKey>(IEntityModelProperty primaryKey)
    {

        MethodCodeBuilder<TModel, TPrimaryKey> codeBuilder = new(typeof(EntityModelBuilder));
        EmitCallConvert(codeBuilder.Argument().GetValue(primaryKey.Model), primaryKey.Model.PropertyType, typeof(TPrimaryKey));
        return codeBuilder.BuildFunc();
    }


    internal static Action<TModel, TEntity> MakeConvertFromModel<TModel, TEntity>(IEnumerable<IEntityModelProperty> properties)
    {
        MethodCodeBuilder codeBuilder = new(typeof(EntityModelBuilder), typeof(void), typeof(TModel), typeof(TEntity));

        foreach (IEntityModelProperty prop in properties)
        {
            using (codeBuilder.If(x => x.Argument().GetValue(prop.Model).Call(s_is_valid,prop.Model.PropertyType)))
            {
                EmitCallConvert(codeBuilder.ArgumentSecond().Argument().GetValue(prop.Model), prop.Model.PropertyType, prop.Entity.PropertyType).SetValue(prop.Entity);
            }
        }

        return codeBuilder.BuildDelegate<Action<TModel, TEntity>>();
    }

    private readonly struct d_property : IEntityModelProperty
    {
        private readonly int _index;
        private readonly PropertyInfo _entity;
        private readonly PropertyInfo _model;

        public d_property(int index , PropertyInfo entity , PropertyInfo model)
        {
            _index = index;
            _entity = entity;
            _model = model;
        }

        public int Index => _index;

        public bool IsIdentity => IdentityAttribute.Is(_entity);

        public PropertyInfo Model => _model;

        public PropertyInfo Entity => _entity;

        public override string ToString() => _entity.Name;
        
    }


    internal static IEnumerable<PropertyInfo> GetProperties(Type type)
    {
        IEnumerable<PropertyInfo> props = type.GetProperties();

        if (type.IsInterface)
        {
            props =  type.GetInterfaces().SelectMany(x => x.GetProperties()).Union(props);
        }

        return props;
    }


    internal static IEnumerable<IEntityModelProperty> Generate<TEntity, TModel>()
    {
        Dictionary<string, PropertyInfo> prop = typeof(TModel).GetProperties().Where(x => x.CanWrite && x.CanRead).ToDictionary(x => x.Name);
        int i = 0;

        List<IEntityModelProperty> list = new();

        foreach (PropertyInfo info in GetProperties(typeof(TEntity)))
        {
            if (prop.TryGetValue(info.Name, out PropertyInfo? model) && model != null)
            {
                list.Add( new d_property(i++, info, model));
            }
        }

        return list;

    }

}

