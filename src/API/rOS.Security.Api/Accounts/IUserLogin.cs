using System;
namespace rOS.Security.Api.Accounts
{
    public interface IUserLogin
    {
        string Name { get; }
        string Password { get; }
    }
}

