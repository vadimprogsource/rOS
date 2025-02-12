using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface IExecuteActionResult
    {
        public bool IsSuccess { get; }
        public bool IsError { get; }

        public bool IsAuth { get; }

        public string? Reason { get; }

        public string? AuthorizeToken { get; }
        public string? RoutePath { get; }

        public IContent? Content { get; }


    }

