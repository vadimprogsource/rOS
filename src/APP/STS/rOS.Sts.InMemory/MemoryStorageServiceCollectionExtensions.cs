
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using rOS.Security.Api.Storages;

namespace rOS.Sts.InMemory;

public static class MemoryStorageServiceCollectionExtensions
{
    public static IServiceCollection AddTokenStorageInMemory(this IServiceCollection @this)
    {
       
        return @this.AddSingleton<ISecurityTokenStorage,SecurityTokenStorage>()
                    .AddSingleton<ISessionStorage,SessionStorage>();
    }
}
