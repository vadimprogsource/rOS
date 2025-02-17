using System;
using System.Linq.Async.Emit;
using System.Reflection;
using System.Reflection.Emit;


namespace Oql.CodeGen;

public class CodeConstructorBuilder : CodeBuilder
{
    private readonly ConstructorBuilder _ctor;

    public CodeConstructorBuilder(TypeBuilder type , params Type[] arguments)
    {
        _ctor = type.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, arguments);
        Generate(_ctor.GetILGenerator());
        
    }


}

