using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;

namespace Wasm.Kernel.Auth;

public class AuthorizeProvider : IAuthorizeProvider
{
    private readonly ILocalStorage _local_storage;
    public AuthorizeProvider(ILocalStorage storage)
    {
        _local_storage = storage;
    }


    public async Task SignInAsync(IExecuteActionResult actionResult)
    {
        await _local_storage.PutAsync("sts_auth_crm24", new AuthorizeToken(actionResult).Token);
    }

    public async Task<IAuthorizeToken> GetTokenAsync()
    {
        return new AuthorizeToken(await _local_storage.GetAsync("sts_auth_crm24"));
    }


    public async Task SignOutAsync()
    {
        await _local_storage.DeleteAsync("sts_auth_crm24");
    }
}
