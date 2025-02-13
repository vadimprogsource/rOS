using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Async.Emit;
public class MethodCodeBuilder : IMethodCodeBuilder
{
 
    private DynamicMethod _delegate;
    private ILGenerator   _gen;

    public MethodCodeBuilder(Type? ownerType, Type returnType, params Type[] argumentTypes)
    {
        ownerType ??= typeof(void);
        _delegate = new($"{ownerType.Name}.{Guid.NewGuid():n}", returnType, argumentTypes ?? Array.Empty<Type>(), ownerType);
        _gen = _delegate.GetILGenerator();
    }
    

    public IMethodCodeBuilder DeclareConstructor(TypeBuilder type, params Type[] arguments)
    {
        ConstructorBuilder ctor = type.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, arguments);

        ConstructorInfo? based = type.BaseType?.GetConstructor(Array.Empty<Type>());

        _gen = ctor.GetILGenerator();

        if (based != null)
        {
            _gen.Emit(OpCodes.Ldarg_0);
            _gen.Emit(OpCodes.Call, based);
        }

        return this;
    }


    public IMethodCodeBuilder This()
    {
        _gen?.Emit(OpCodes.Dup);
        return this;
    }


    public IMethodCodeBuilder Argument()
    {
        _gen?.Emit(OpCodes.Ldarg_0);
        return this;
    }


    public IMethodCodeBuilder ArgumentSecond()
    {
        _gen?.Emit(OpCodes.Ldarg_1);
        return this;
    }


    public IMethodCodeBuilder NewObject<T>() => NewObject(typeof(T));


    public IMethodCodeBuilder NewObject(Type type)
    {
        _gen?.Emit(OpCodes.Newobj, type.GetConstructor(Array.Empty<Type>()) ?? type.GetConstructors().First());
        return this;
    }


    public IMethodCodeBuilder NewObject(ConstructorInfo ctor)
    {
        _gen?.Emit(OpCodes.Newobj, ctor);
        return this;
    }


    private class IfBlock : IDisposable
    {
        private readonly ILGenerator _gen;
        private readonly Label _label;

        public IfBlock(ILGenerator gen)
        {
            _gen = gen;
            _label = gen.DefineLabel();
            gen.Emit(OpCodes.Brfalse, _label);
        }

        public void Dispose()
        {
            _gen.MarkLabel(_label);
        }
    }

    public IDisposable If(Action<IMethodCodeBuilder> condition)
    {
        condition(this);
        return new IfBlock(_gen ?? throw new NullReferenceException());
    }



    public IMethodCodeBuilder GetValue(FieldInfo field)
    {
        _gen?.Emit(OpCodes.Ldfld, field);
        return this;
    }


    public IMethodCodeBuilder SetValue(FieldInfo field)
    {
        _gen?.Emit(OpCodes.Stfld, field);
        return this;
    }


    private MethodCodeBuilder EmitCallMethod(MethodInfo method)
    {
        if (method.DeclaringType != null)
        {
            _gen?.Emit(method.IsVirtual || method.DeclaringType.IsInterface ? OpCodes.Callvirt : OpCodes.Call, method);
        }

        return this;
    }

    public IMethodCodeBuilder GetValue(PropertyInfo property) => EmitCallMethod(property.GetGetMethod() ?? throw new NullReferenceException());

    public IMethodCodeBuilder SetValue(PropertyInfo property) => EmitCallMethod(property.GetSetMethod() ?? throw new NullReferenceException());



    public IMethodCodeBuilder GetValue(MemberInfo member)
    {

        if (member.MemberType == MemberTypes.Field && member is FieldInfo field)
        {
            return GetValue(field);
        }

        if (member.MemberType == MemberTypes.Property && member is PropertyInfo property)
        {
            return GetValue(property);
        }

        throw new NotSupportedException();
    }

    public IMethodCodeBuilder SetValue(MemberInfo member)
    {
        if (member.MemberType == MemberTypes.Field && member is FieldInfo field)
        {
            return SetValue(field);
        }

        if (member.MemberType == MemberTypes.Property && member is PropertyInfo property)
        {
            return SetValue(property);
        }

        throw new NotSupportedException();
    }



    public Delegate BuildDelegate(Type methodType)
    {
        Build();
        return _delegate.CreateDelegate(methodType);
    }

    public TDelegate BuildDelegate<TDelegate>() => BuildDelegate(typeof(TDelegate)) is TDelegate d ? d : throw new NotSupportedException();

    public IMethodCodeBuilder Call(MethodInfo method) => EmitCallMethod(method);



    public void Dispose()
    {
        Build();
    }

    public IMethodCodeBuilder ReadField(FieldInfo field, MethodInfo read) => Argument().ArgumentSecond().Call(read).SetValue(field);
    public IMethodCodeBuilder ReadProperty(PropertyInfo prop, MethodInfo read) => Argument().ArgumentSecond().Call(read).SetValue(prop);

    public IMethodCodeBuilder Build()
    {
        _gen.Emit(OpCodes.Ret);
        return this;
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }
}

public class MethodCodeBuilder<TArgument, TResult> : MethodCodeBuilder, IMethodCodeBuilder<TArgument, TResult>
{
    public MethodCodeBuilder(Type? ownerType = null) : base(ownerType, typeof(TResult), typeof(TArgument))
    {

    }

    public Func<TArgument, TResult> BuildFunc() => BuildDelegate<Func<TArgument, TResult>>();
}



