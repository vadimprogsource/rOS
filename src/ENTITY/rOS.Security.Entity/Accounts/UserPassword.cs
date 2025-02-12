using System;
using System.CoreLib.Builders;
using rOS.Security.Api.Accounts;

namespace rOS.Security.Entity.Accounts;

public readonly struct UserPassword : IUserPassword
{
    private readonly Guid   _guid;
    private readonly byte[] _hash;



    public Guid Guid => _guid;

    public byte[] Hash => _hash;


    public UserPassword(string login, string password)
    {
        _guid = PasswordBuilder.MakeGuid(login, password);
        _hash = PasswordBuilder.MakePasswordHash(login, password);
    }


    public bool IsEqualTo(IUserPassword password) =>   _guid == password.Guid && PasswordBuilder.Compare(_hash, password.Hash);
    
}

