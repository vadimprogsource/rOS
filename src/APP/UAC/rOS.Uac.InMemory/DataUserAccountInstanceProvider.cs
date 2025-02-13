using System;
using Oql.Api.Runtime;
using rOS.Security.Api.Accounts;
using rOS.Security.Entity.Accounts;

namespace rOS.Uac.InMemory;

public class DataUserAccountInstanceProvider : IInstanceProvider<IUserAccount,UserAccount>
{
    public UserAccount GetInstance(IUserAccount ifc)
    {
        if (ifc is UserAccount user) return user;
        return new DataUserAccount(ifc);
    }
}

