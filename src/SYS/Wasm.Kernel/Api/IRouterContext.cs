using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface  IRouterContext
    {
        void ExecuteRoute(IAction action);
        string Page { get;}
    }

