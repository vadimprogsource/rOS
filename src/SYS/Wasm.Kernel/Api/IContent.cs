using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface IContent
    {
        Task<TContent?> GetContentAsync<TContent>();
        Task<IError[]?> GetErrorsAsync();
    }


    public interface IContentReader
    {
        Task<object?> ReadAsync(IContent content); 
    }

