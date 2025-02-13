using Microsoft.Extensions.DependencyInjection;
using Oql.Api.Runtime;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Storages;
using rOS.Security.Entity.Accounts;

namespace rOS.Uac.InMemory;

public static class MemoryStorageServiceCollectionExtensions
{
    public static IServiceCollection AddUserAccountStorageInMemory(this IServiceCollection @this)
    {

        return @this.AddSingleton<IUserAccountStorage, DataUserAccountStorage>()
                    .AddSingleton<IFactory<UserAccount>, DataUserAccountFactory>()
                    .AddSingleton<IInstanceProvider<IUserAccount,UserAccount>, DataUserAccountInstanceProvider>();
    }
}

