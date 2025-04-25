using System.Reflection;
using System.Reflection.Emit;

namespace Oql.CodeGen;

public class CodeTypeBuilder
{
    private readonly static AssemblyBuilder s_asm_builder   ;
    private readonly static ModuleBuilder   s_module_builder ;


    static CodeTypeBuilder()
    {
        s_asm_builder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName($"{typeof(CodeTypeBuilder)}-{Guid.NewGuid()}"), AssemblyBuilderAccess.RunAndCollect);
        s_module_builder = s_asm_builder.DefineDynamicModule("module");
    }

    private readonly TypeBuilder  _type_builder ;


    public CodeTypeBuilder(string name)
    {
      _type_builder =  s_module_builder.DefineType(name + Guid.NewGuid().ToString(), TypeAttributes.Class | TypeAttributes.Public);

        CodeConstructorBuilder ctor = new(_type_builder);
        ctor.Build();
    }

    public CodeTypeBuilder AutoProperty(string name, Type type)
    {
        FieldBuilder field       = _type_builder.DefineField("_" + name.ToLowerInvariant(), type, FieldAttributes.Private);
        PropertyBuilder property = _type_builder.DefineProperty(name, PropertyAttributes.HasDefault, type,null);

        property.SetGetMethod(CodeMethodBuilder.CreateGetMethod(_type_builder, field));
        property.SetSetMethod(CodeMethodBuilder.CreateSetMethod(_type_builder, field));

        return this;
    }

    public Type Buid() => _type_builder.CreateType();
}

