using System;
using System.CoreLib.Builders;
using rOS.Security.Api.Accounts;

namespace rOS.Security.Entity.Accounts;

public readonly struct UserPassword : IUserPassword
{
    private readonly string _name;
    private readonly Guid   _guid;
    private readonly byte[] _hash;



    public Guid Guid => _guid;

    public byte[] Hash => _hash;

    public string Name => _name;

    public string Password => string.Empty;

    public UserPassword(string login, string password)
    {
        _name = login;
        _guid = PasswordBuilder.MakeGuid(login, password);
        _hash = PasswordBuilder.MakePasswordHash(login, password);
    }

    public UserPassword(IUserLogin login) : this(login.Name, login.Password) { }


    public bool IsEqualTo(IUserPassword password) =>   _guid == password.Guid && PasswordBuilder.Compare(_hash, password.Hash);
    
}

