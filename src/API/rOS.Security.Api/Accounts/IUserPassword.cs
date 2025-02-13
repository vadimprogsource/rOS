using System;
namespace rOS.Security.Api.Accounts
{
    public interface IUserPassword : IUserLogin
    {
        Guid Guid { get; }
        byte[] Hash { get; }

        bool IsEqualTo(IUserPassword password);
    }
}

