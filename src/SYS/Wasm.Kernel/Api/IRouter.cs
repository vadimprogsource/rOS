using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface IRouter
    {
        IRouter Map<T>(string routePath, string action,string? method=null,string? route=null);

        TContent? Pop<TContent>();

        Task RouteAsync(IRouterContext context , string routePath ,Guid? guid, IContent? content);

        IAction? GetCurrentAction(IRouterContext context);
        IAction? GetSafeAction(IAction? action);

        string MakePath(string? path, Guid? guid = null);
        string MakePath(string? uri , string? relativePath );
    }

