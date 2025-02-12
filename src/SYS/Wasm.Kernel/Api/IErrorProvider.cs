using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface  IErrorProvider
    {
        Task ThrowErrorsAsync(IExecuteActionResult actionResult);
        void ThrowException(Exception exception);
        void Success();
        bool HasErrors { get; }
        bool IsError(string propertyName);
        IError? GetError(string propertyName);
        IError[]? GetErrors();
      
    }

