using System;
namespace rOS.Security.Api.Accounts
{
    public interface IUserPassword
    {
        Guid Guid { get; }
        byte[] Hash { get; }

        bool IsEqualTo(IUserPassword password);
    }
}

