using System;
using System.Reflection;

namespace Oql.Api.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class IdentityAttribute : Attribute
{

    private static bool Is_Def_Name(string name)
    {
        return name.ToLowerInvariant() switch
        {
            "guid" or "uid" or "uuid" or "id" => true,
            _ => false,
        };
    }


    public IdentityAttribute()
    {
    }

    public static bool Is(MemberInfo member)
    {
        if (IsDefined(member, typeof(IdentityAttribute)))
        {
            return true;
        }

        if (member is PropertyInfo pi)
        {
            if (pi.PropertyType == typeof(Guid) && string.Compare("guid", pi.Name, true) == 0)
            {
                return true;
            }

            if ((pi.PropertyType == typeof(int) || pi.PropertyType == typeof(long)) && string.Compare("id", pi.Name, true) == 0)
            {
                return true;
            }
        }


        return false;
    }



    public static bool IsDefault(PropertyInfo prop)
    {



        if (prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid?))
        {
            return Is_Def_Name(prop.Name);
        }


        return Type.GetTypeCode(prop.PropertyType) switch
        {
            TypeCode.Int32 or TypeCode.UInt32 or TypeCode.Int64 or TypeCode.UInt64 or TypeCode.Decimal => Is_Def_Name(prop.Name),
            _ => false,
        };
    }
}

