using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface IAuthorizeProvider
    {
        Task SignInAsync(IExecuteActionResult actionResult);
        Task<IAuthorizeToken> GetTokenAsync();
        Task SignOutAsync();
    }

