using System;
using Oql.Api.Runtime;
using rOS.Security.Entity.Accounts;

namespace rOS.Uac.InMemory
{
    public class DataUserAccountFactory : IFactory<UserAccount>
    {

        public Task<UserAccount> CreateInstance() => Task.FromResult<UserAccount>( new DataUserAccount() );
        
    }
}

