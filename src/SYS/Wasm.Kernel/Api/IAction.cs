using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface IAction
    {
        Guid? Guid { get; }
        string? Action { get; }
        string? Method { get;}
        string? Route { get;  }
        object? Model { get;  }
    }

