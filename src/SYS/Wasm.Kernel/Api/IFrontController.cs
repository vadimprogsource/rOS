using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface IFrontController
    {
       Task<TModel?> FetchModelAsync<TModel>(HttpMethod method, string path, object? model=null);
       Task<TModel?> FetchModelAsync<TModel>(IAction? action, string relativePath, object? model = null);


    Task<TModel?> GetModelAsync<TModel>(IAction? action);
        Task<IExecuteActionResult> ExecuteActionAsync(IAction? action);
    }

