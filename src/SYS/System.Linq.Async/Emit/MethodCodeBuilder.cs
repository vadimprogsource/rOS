using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Async.Emit;
public  class MethodCodeBuilder :CodeBuilder  ,  IMethodCodeBuilder
{
 
    private readonly DynamicMethod method;


    public MethodCodeBuilder(Type? ownerType, Type returnType, params Type[] argumentTypes)  : base()
    {
        ownerType ??= typeof(void);
        method = new($"{ownerType.Name}.{Guid.NewGuid():n}", returnType, argumentTypes ?? Array.Empty<Type>(), ownerType);
        Generate(method.GetILGenerator());
    }
    


    public Delegate BuildDelegate(Type methodType)
    {
        Build();
        return method.CreateDelegate(methodType);
    }

    public TDelegate BuildDelegate<TDelegate>() => BuildDelegate(typeof(TDelegate)) is TDelegate d ? d : throw new NotSupportedException();

}

public class MethodCodeBuilder<TArgument, TResult> : MethodCodeBuilder, IMethodCodeBuilder<TArgument, TResult>
{
    public MethodCodeBuilder(Type? ownerType = null) : base(ownerType, typeof(TResult), typeof(TArgument))
    {

    }

    public Func<TArgument, TResult> BuildFunc() => BuildDelegate<Func<TArgument, TResult>>();
}



