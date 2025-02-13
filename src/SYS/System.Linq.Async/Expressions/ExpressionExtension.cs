using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq.Async.Expressions;

public static class ExpressionExtentions
{



    public static IEnumerable<MemberInfo> SelectMembers(this Type @base)
    {
        if (@base.IsInterface)
        {
            return @base.GetInterfaces()
                        .SelectMany(x => x.GetProperties())
                        .Union(@base.GetProperties())
                        .OfType<MemberInfo>();
        }

        return @base.GetMembers().Where(x => x.MemberType == MemberTypes.Property);
    }
    public static Type? GetQueryType(this Expression @this)
    {
        for (Expression x = @this; x != null;)
        {
            if (x.NodeType == ExpressionType.Call && x is MethodCallExpression m)
            {
                x = m.Arguments.First();
                continue;
            }

            if (x.NodeType == ExpressionType.Constant && x is ConstantExpression c && c.Value is IQueryable q)
            {
                return q.ElementType;
            }

            break;
        }

        return default;
    }

    public static bool IsGrouping(this Type @this) => @this.IsGenericType && @this.GetGenericTypeDefinition() == typeof(IGrouping<,>);


    public static bool IsNull(this Expression @this)
    {
        if (@this == null)
        {
            return true;
        }

        if (@this.NodeType == ExpressionType.Constant && @this is ConstantExpression c)
        {
            return c.Value == null;
        }

        return false;
    }

    public static bool IsPropertyOrField(this Expression @this)
    {
        for (Expression? x = @this; x != null;)
        {
            if (x is MemberExpression m)
            {
                x = m.Expression;
                continue;
            }

            if (x.NodeType == ExpressionType.Parameter)
            {
                return true;
            }

            break;
        }

        return false;

    }


    public static Stack<MemberExpression> GetCallMemberStack(this MemberExpression @this)
    {
        Stack<MemberExpression> path = new();

        for (MemberExpression? x = @this; x != null; x = x.Expression as MemberExpression)
        {
            path.Push(x);
        }

        return path;
    }


    public static IEnumerable<Expression> GetExpressionIterator(this Expression @this)
    {
        for (Expression? x = @this; x != null;)
        {
            yield return x;

            if (x.NodeType == ExpressionType.MemberAccess && x is MemberExpression m)
            {
                x = m.Expression;
                continue;
            }

            if (x.NodeType == ExpressionType.Lambda && x is LambdaExpression l)
            {
                x = l.Body;
                continue;
            }

            if (x is UnaryExpression u)
            {
                x = u.Operand;
                continue;
            }

            if (x.NodeType == ExpressionType.Call && x is MethodCallExpression c)
            {
                x = c.Arguments.First();
                continue;
            }


            if (x is BinaryExpression b)
            {
                x = b.Left;
                continue;
            }

            break;
        }
    }


    public static MemberExpression? GetMemberExpression(this Expression @this) => @this
          .GetExpressionIterator()
          .Where(x => x.NodeType == ExpressionType.MemberAccess)
          .OfType<MemberExpression>()
           .FirstOrDefault();



    public static LambdaExpression? GetLambda(this Expression @this) => @this
          .GetExpressionIterator()
          .Where(x => x.NodeType == ExpressionType.Lambda)
          .OfType<LambdaExpression>()
           .FirstOrDefault();


    public static object? GetValue(this Expression @this)
    {
        if (@this.NodeType == ExpressionType.Constant && @this is ConstantExpression @const)
        {
            return @const.Value;
        }

        return Expression.Lambda(@this).Compile().DynamicInvoke();
    }



    public static Expression GetInnerExpression(this Expression @this)
    {
        if (@this.NodeType == ExpressionType.Quote && @this is UnaryExpression unary)
        {
            @this = unary.Operand;
        }

        if (@this.NodeType == ExpressionType.Lambda && @this is LambdaExpression lambda )
        {
            @this = lambda.Body;
        }

        return @this;
    }




    public static Expression GetArgument(this MethodCallExpression @this, int index)
    {
        return @this.Arguments[index].GetInnerExpression();
    }


    public static MethodInfo GetBaseMethod(this MethodInfo @this)
    {
        return @this.IsGenericMethod ? @this.GetGenericMethodDefinition() : @this;
    }



    public static Type GetQueryElementType(this Type @this)
    {
        if (@this.IsInterface && @this.GetGenericTypeDefinition() == typeof(IQueryable<>))
        {
            return @this.GetGenericArguments().Single();
        }

        return @this;
    }


    public static bool CompareTo(this Array @this, Array array)
    {

        if (@this == null && array == null)
        {
            return true;
        }

        if (@this == null || array == null || @this.LongLength != array.LongLength || @this.Rank != array.Rank)
        {
            return false;
        }

        if (@this.Length < 1)
        {
            return true;
        }


        for (long i = 0, j = array.LongLength - 1; i < j; i++, j--)
        {
            if (Equals(@this.GetValue(i), array.GetValue(i)) && Equals(@this.GetValue(j), array.GetValue(j)))
            {
                continue;
            }

            return false;
        }

        return true;
    }

}

