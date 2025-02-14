using System;
using System.Linq.Async.Emit;
using System.Linq.Async.Reflection;
using System.Reflection;
using Oql.Api.Runtime;

namespace Oql.Runtime;



public class EntityModelProvider
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

    public static bool IsValid<T>(T value) => value != null;

    internal static IMethod s_convert_to_string = new Method<EntityModelProvider>(x => ConvertToString(1));
    internal static IMethod s_convert_to        = new Method<EntityModelProvider>(x => ConvertTo<int, string>(int.MinValue));
    internal static IMethod s_is_valid          = new Method<EntityModelProvider>(x => IsValid(int.MinValue));





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

        return codeBuilder.Call(s_convert_to,sourceType, targetType);
    }


    internal static Func<TEntity, TModel> MakeConvertToModel<TEntity, TModel>(IEnumerable<IEntityModelProperty> properties)
    {
        MethodCodeBuilder<TEntity, TModel> codeBuilder = new(typeof(EntityModelProvider));
        codeBuilder.NewObject<TModel>();

        foreach (IEntityModelProperty prop in properties)
        {
            EmitCallConvert(codeBuilder.This().Argument().GetValue(prop.Entity), prop.Entity.PropertyType, prop.Model.PropertyType).SetValue(prop.Model);
        }

        return codeBuilder.BuildFunc();
    }


    internal static Func<TModel, TPrimaryKey> MakeGetPrimaryKey<TModel, TPrimaryKey>(IEntityModelProperty primaryKey)
    {

        MethodCodeBuilder<TModel, TPrimaryKey> codeBuilder = new(typeof(EntityModelProvider));
        EmitCallConvert(codeBuilder.Argument().GetValue(primaryKey.Model), primaryKey.Model.PropertyType, typeof(TPrimaryKey));
        return codeBuilder.BuildFunc();
    }


    internal static Action<TModel, TEntity> MakeConvertFromModel<TModel, TEntity>(IEnumerable<IEntityModelProperty> properties)
    {
        MethodCodeBuilder codeBuilder = new(typeof(EntityModelProvider), typeof(void), typeof(TModel), typeof(TEntity));

        foreach (IEntityModelProperty prop in properties)
        {
            using (codeBuilder.If(x => x.Argument().GetValue(prop.Model).Call(s_is_valid,prop.Model.PropertyType)))
            {
                EmitCallConvert(codeBuilder.ArgumentSecond().Argument().GetValue(prop.Model), prop.Model.PropertyType, prop.Entity.PropertyType).SetValue(prop.Entity);
            }
        }

        return codeBuilder.BuildDelegate<Action<TModel, TEntity>>();
    }

}

