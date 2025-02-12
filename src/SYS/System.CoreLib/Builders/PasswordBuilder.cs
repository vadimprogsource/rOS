using System;
using System.Security.Cryptography;
using System.Text;

namespace System.CoreLib.Builders;

public static class PasswordBuilder 
{

    private static byte[] GetBytes(params object[] args)
    {
        StringBuilder sb = new();
        foreach (object arg in args)
        {
            sb.Append(arg);
        }

        return Encoding.ASCII.GetBytes(sb.ToString());
    }


    public static byte[] MakePasswordHash(params object[] args) => SHA256.HashData(GetBytes(args));
    public static Guid MakeGuid(params object[] args) => new(MD5.HashData(GetBytes(args)));

    public static bool ComparePasswordHash(byte[] passwordHash, params object[] args) => Compare(passwordHash, MakePasswordHash(args));


    public static bool Compare(byte[] a, byte[] b)
    {

        if (a.Length == b.Length)
        {
            for (int i = 0, j = b.Length - 1; i <= j; i++, j--)
            {
                if (b[i] != a[i] || b[j] != a[j])
                {
                    return false;
                }
            }

            return true;
        }

        return false;

    }

}


