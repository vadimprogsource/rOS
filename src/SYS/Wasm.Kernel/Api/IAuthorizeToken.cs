using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface IAuthorizeToken
    {
        void Intialize(IExecuteActionResult actionResult);
        bool IsAuthorized { get; }
        string? Token { get; }
    }

