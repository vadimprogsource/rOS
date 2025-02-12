using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;

namespace Wasm.Kernel.Routers;


public class Router : IRouter
{


    abstract class RouteInfo : IContentReader , IAction
    {
        public string? Action { get; }

        public string? Method { get; }

        public string? Route { get; }

        public object? Model => null;

        public Guid? Guid { get; private set; }

        public abstract Task<object?> ReadAsync(IContent content);


        public RouteInfo(string action, string? method,string? route)
        {
            Action = action;
            Method = method;
            Route = route;
        }


        public IAction Combine(Guid? guid)
        {
            if (guid is not null && MemberwiseClone() is RouteInfo @this)
            {
                @this.Guid = guid;
                return @this;
            }

            return this;
        }

        public IAction Combine(IAction action)=>Combine(action.Guid);
        
    }

    class RouteInfo<T> : RouteInfo
    {
        public RouteInfo(string action,string? method,string? route) : base(action,method,route)
        {
        }

        public async override Task<object?> ReadAsync(IContent content)
        {
            return await content.GetContentAsync<T>();
        }
    }


    class RouteGuid : IAction
    {

        public string? Action { get; set; }

        public string? Method { get; set; }

        public string? Route { get; set; }

        public object? Model => default;
        public Guid? Guid { get; set; }

        public RouteGuid(RouteInfo info,Guid guid)
        {

            Guid = guid;

            Action = info.Action;
            Route  = info.Route;
        }

    }


    class DefAction : IAction
    {
        public Guid? Guid => default;

        public string? Action => "/";

        public string? Method => "get";

        public virtual string? Route => "/";

        public object? Model => default;
    }

    class LocalAction : DefAction
    {
        private readonly string m_route_path;

        public override string? Route => m_route_path;

        public LocalAction(string? route) => m_route_path = route ?? "/";
    }


    private static readonly IAction DefaultAction = new DefAction();


    private readonly Dictionary<string, RouteInfo> m_route_info = new();

    private  object? m_content = null;
    public IRouter Map<T>(string key, string action, string? method = null,string? route=null)
    {
        m_route_info.Add(key, new RouteInfo<T>(action,method, route??key));
        return this;
    }

    public TContent? Pop<TContent>()
    {

        if (m_content is TContent c)
        {
            m_content = null;
            return c;
        }


        return default;

    }

    public async Task RouteAsync(IRouterContext context, string routePath,Guid? guid, IContent? content)
    {
        if (m_route_info.TryGetValue(routePath, out RouteInfo? info))
        {

            if (content == null)
            {
                m_content = null;
            }
            else
            {
                m_content = await info.ReadAsync(content);
            }

            context.ExecuteRoute(info.Combine(guid));
            return;
        }

        context.ExecuteRoute(new LocalAction(routePath));
    }

    public IAction? GetCurrentAction(IRouterContext context)
    {

        if (m_route_info.TryGetValue(context.Page, out RouteInfo? info))
        {
            return info;
        }
        else
        {
            string[] seg = context.Page.Split('/', '\\').Where(x=>!string.IsNullOrWhiteSpace(x)).ToArray();

            if (seg.Length > 1)
            {
                StringBuilder sb = new();

                foreach (string s in seg.Take(seg.Length - 1))
                {
                    sb.Append('/').Append(s);
                }

                if (m_route_info.TryGetValue(sb.ToString(), out info))
                {
                    return info.Combine(Guid.Parse( seg.Last())) ;

                }
            }
        }

        return default;
    }

    public IAction? GetSafeAction(IAction? action)
    {


        if (action == null)
        {
            return DefaultAction;
        }

        if (string.IsNullOrWhiteSpace(action.Route) || !string.IsNullOrWhiteSpace(action.Action))
        {
            return action;
        }


        if (m_route_info.TryGetValue(action.Route, out RouteInfo? info))
        {
            return info.Combine(action);
        }

        return action;

    }

    public string MakePath(string? path, Guid? guid = null)
    {

        if (guid is null)
        {
            return path ?? "/";
        }

        return $"{path ?? "/"}/{guid:n}";
    }

    public string MakePath(string? uri, string? relativePath = null)
    {
        if (relativePath is null || string.IsNullOrWhiteSpace(relativePath))
        {
            return uri ?? "/";
        }

        return $"{uri ?? "/"}/{relativePath}";
    }

}
