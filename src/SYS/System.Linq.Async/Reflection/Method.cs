using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq.Async.Reflection;

public class Method : IMethod
{

    public static IEnumerable<Method> GetMethodsByName(Type type, string methodName)
    {
        return type.GetMember(methodName).Where(x => x.MemberType == MemberTypes.Method).OfType<MethodInfo>().Select(x => new Method(x));
    }


    public static MethodInfo GetKeyMethod(MethodInfo m)
    {
        if (m.IsGenericMethod)
        {
            return m.GetGenericMethodDefinition();
        }

        return m;
    }

    public MethodInfo GetMethodInfo()
    {
        return m_method_info;
    }


    private MethodInfo m_method_info;

    public Method(MethodInfo methodInfo)
    {
        m_method_info = GetKeyMethod(methodInfo);
    }


    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is Method m)
        {
            return m.m_method_info == m_method_info;
        }

        if (obj is MethodCallExpression expr)
        {
            return m_method_info == GetKeyMethod((expr).Method);
        }

        if (obj is MethodInfo mi)
        {
            return m_method_info == GetKeyMethod(mi);
        }

        return false;
    }

    public bool Is(MethodCallExpression methodCall)
    {

        if (m_method_info.IsGenericMethodDefinition)
        {
            return m_method_info == GetKeyMethod(methodCall.Method);
        }

        return m_method_info == methodCall.Method || m_method_info.Name == methodCall.Method.Name;

    }



    public override int GetHashCode()
    {
        return typeof(Method).GetHashCode() ^ m_method_info.GetHashCode();
    }


    public Expression Call<T>(Expression operand)
    {
        return Expression.Call(m_method_info.MakeGenericMethod(typeof(T)), operand);
    }

    public Expression Call<T>(Expression left, LambdaExpression right)
    {
        return Expression.Call(m_method_info.MakeGenericMethod(typeof(T)), left, Expression.Quote(right));
    }

    public Expression Call<T>(Expression left, ConstantExpression right)
    {
        return Expression.Call(m_method_info.MakeGenericMethod(typeof(T)), left, right);
    }

    public Expression Call<T, V>(Expression left, LambdaExpression right)
    {
        return Expression.Call(m_method_info.MakeGenericMethod(typeof(T), typeof(V)), left, Expression.Quote(right));
    }


    public Expression Call<T, V>(Expression @this, LambdaExpression left, LambdaExpression right)
    {
        return Expression.Call(m_method_info.MakeGenericMethod(typeof(T), typeof(V)), @this, Expression.Quote(left), Expression.Quote(right));
    }

    public MethodInfo GetBaseMethod()
    {
        return GetKeyMethod(m_method_info);
    }

    public MethodInfo MakeGenericMethod(params Type[] type) => m_method_info.MakeGenericMethod(type);

}

public class Method<T> : Method
{
    public Method(Expression<Action<T>> methodCall) : base((methodCall.Body is MethodCallExpression call)?call.Method:throw new NotSupportedException()) { }
}

