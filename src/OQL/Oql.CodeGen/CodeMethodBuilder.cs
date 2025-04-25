using System;
using System.Linq.Async.Emit;
using System.Reflection;
using System.Reflection.Emit;

namespace Oql.CodeGen;

public class CodeMethodBuilder : CodeBuilder
{

    public static MethodBuilder CreateGetMethod(TypeBuilder type, FieldInfo field)
    {
        CodeMethodBuilder builder = new(type, "get" + field.Name,field.FieldType);
        builder.Argument().GetValue(field);
        return builder.Build();
    }

    public static MethodBuilder CreateSetMethod(TypeBuilder type, FieldInfo field)
    {
        CodeMethodBuilder builder = new(type, "set" + field.Name, null,field.FieldType);
        builder.Argument().ArgumentSecond().SetValue(field);
        return builder.Build();
    }

    private readonly MethodBuilder _method;

    public CodeMethodBuilder(TypeBuilder type , string name, Type? returnType , params Type[] argumentTypes)
    {
        _method = type.DefineMethod(name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, returnType, argumentTypes);
        Generate(_method.GetILGenerator());

    }

    public new MethodBuilder Build()
    {
        base.Build();
        return _method;
    }
}

