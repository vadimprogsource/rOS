using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;


    public interface IError
    {
        bool IsWarning { get; }

        string PropertyName { get; }
        int ErrorStatus { get; }
        string? ErrorCode { get; }
        string? Message { get; }
    }



