using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Async.Emit;

public interface IMethodCodeBuilder : IDisposable,IAsyncDisposable
{


    IMethodCodeBuilder DeclareConstructor(TypeBuilder type, params Type[] arguments);



    IDisposable If(Action<IMethodCodeBuilder> condition);

    IMethodCodeBuilder This();
    IMethodCodeBuilder Argument();
    IMethodCodeBuilder ArgumentSecond();
    IMethodCodeBuilder NewObject<T>();
    IMethodCodeBuilder NewObject(Type type);
    IMethodCodeBuilder NewObject(ConstructorInfo ctor);
    IMethodCodeBuilder GetValue(FieldInfo field);
    IMethodCodeBuilder GetValue(PropertyInfo property);
    IMethodCodeBuilder GetValue(MemberInfo member);
    IMethodCodeBuilder SetValue(FieldInfo field);
    IMethodCodeBuilder SetValue(PropertyInfo property);
    IMethodCodeBuilder SetValue(MemberInfo member);

    IMethodCodeBuilder ReadField(FieldInfo field, MethodInfo read);
    IMethodCodeBuilder ReadProperty(PropertyInfo prop, MethodInfo read);

    IMethodCodeBuilder Call(MethodInfo method);


    Delegate BuildDelegate(Type methodType);
    TDelegate BuildDelegate<TDelegate>();




}


public interface IMethodCodeBuilder<TArgument, TResult> : IMethodCodeBuilder
{
    Func<TArgument, TResult> BuildFunc();
}

