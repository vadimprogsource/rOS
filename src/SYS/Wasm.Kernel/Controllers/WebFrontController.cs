using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using System.Net.Http.Headers;
using Wasm.Kernel.Api;
using Wasm.Kernel.Models;

namespace Wasm.Kernel.Controllers;



public class WebFrontController : IFrontController,IRouterContext
{
    private readonly HttpClient         _http_client;
    private readonly NavigationManager  _router_context;
    private readonly IRouter            _router;
    private readonly IAuthorizeProvider _auth_provider;
    private readonly IErrorProvider     _error_provider;

    public string Page => new Uri(_router_context.Uri).PathAndQuery;

    public WebFrontController(HttpClient httpClient,  NavigationManager navigation,IRouter router , IAuthorizeProvider auth , IErrorProvider errorProvider)
    {
        _http_client = httpClient;
        _router_context = navigation;
        _router = router;
        _auth_provider = auth;
        _error_provider = errorProvider;
    }


    private class ExecuteActionResult : IExecuteActionResult ,IContent
    {

        private  HttpContent? m_cached_content;

        public ExecuteActionResult()
        {
            m_cached_content = null;
            AuthorizeToken = null;
            RoutePath = null;
            Reason = null;
            IsSuccess = false;
        }


        private string? TryGetAuthorizeToken(HttpResponseHeaders headers)
        {
            if (headers !=null &&  headers.TryGetValues("authorization", out IEnumerable<string>? a))
            {
                return a.First();
            }

            return null;
        }



        public ExecuteActionResult(HttpResponseMessage response) : this()
        {

            m_cached_content = response.Content;
            AuthorizeToken = TryGetAuthorizeToken(response.Headers);

            if (response.IsSuccessStatusCode)
            {
                if (response.Headers.TryGetValues("command-path", out IEnumerable<string>? c))
                {
                    RoutePath = c.First();
                }
                else
                {
                    RoutePath = null;
                }

                IsSuccess = true;
            }
        }



        public ExecuteActionResult(Exception err) : this()
        {
            Reason = err.Message;
        }



        public ExecuteActionResult(string errReason) : this()
        {
            Reason =errReason;
        }
        public bool IsSuccess { get;  }

        public bool IsError => !IsSuccess;

        public bool IsAuth => !string.IsNullOrWhiteSpace(AuthorizeToken);

        public string? Reason { get;  }

        public string? AuthorizeToken { get; }

        public string? RoutePath { get;  }


        public IContent? Content => m_cached_content == null ? default : this;




        public async Task<TContent?> GetContentAsync<TContent>()
        {

            if (m_cached_content == null)
            {
                return default;
            }

            return await m_cached_content.ReadFromJsonAsync<TContent>();
        }




 

        public async Task<IError[]?> GetErrorsAsync()
        {

            if (m_cached_content == null) return default;

            ErrorContentModel? ec = await m_cached_content.ReadFromJsonAsync<ErrorContentModel>();

            if (ec == null)
            {
                return default;
            }

            if (ec.HasErrors)
            {
                return ec.Errors;
            }

            return Array.Empty<IError>();
                 


        }
    }




    protected async Task<IExecuteActionResult> ExecuteRequestAsync(IAction action)
    {
        return await ExecuteRequestAsync(new HttpMethod(action.Method??"get"), _router.MakePath(action.Action , action.Guid), action.Model);
    }



    public async Task<TModel?> FetchModelAsync<TModel>(HttpMethod method, string path, object? model = null)
    {
        IExecuteActionResult actionResult = await ExecuteRequestAsync(method,path,model);

        if (actionResult.IsSuccess && actionResult.Content != null)
        {
            return await actionResult.Content.GetContentAsync<TModel>();
        }

        return default;
    }

    protected async Task<IExecuteActionResult> ExecuteRequestAsync(HttpMethod method, string path ,  object? model = null)
    {

        try
        {
            
            HttpRequestMessage request = new(method, path);


            IAuthorizeToken token = await _auth_provider.GetTokenAsync();

            if (token.IsAuthorized)
            {
                _http_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.Token ?? string.Empty);
            }
            else
            {
                _http_client.DefaultRequestHeaders.Authorization = null;
            }


            if (model != null)
            {
                request.Content = JsonContent.Create(model);
            }

            _error_provider.Success();

            ExecuteActionResult actionResult = new(await _http_client.SendAsync(request));


            if (actionResult.IsError)
            {
                await _error_provider.ThrowErrorsAsync(actionResult);
            }

            if (actionResult.IsAuth)
            {
                await _auth_provider.SignInAsync(actionResult);

            }
            else
            {
                await _auth_provider.SignOutAsync();
            }

            return actionResult;
        }
        catch (Exception err)
        {
            _error_provider.ThrowException(err);
            await _auth_provider.SignOutAsync();
            return new ExecuteActionResult(err);
        }

    }


    public async Task<TModel?> GetModelAsync<TModel>(IAction? action)
    {


        TModel? model = _router.Pop<TModel>();

        if (model == null)
        {

            if (action == null || string.IsNullOrWhiteSpace(action.Action))
            {
                action = _router.GetCurrentAction(this);
            }


            if (action != null)
            {
                IExecuteActionResult actionResult = await ExecuteRequestAsync(action);

                if (actionResult.Content != null)
                {
                    model = await actionResult.Content.GetContentAsync<TModel>();
                }
            }

        }

        return model;
    }


    public async Task<IExecuteActionResult> ExecuteActionAsync(IAction? action)
    {

        action = _router.GetSafeAction(action);

        if (action == null)
        {
            return new ExecuteActionResult("Action cannot be NULL!");
        }


        IExecuteActionResult actionResult = await ExecuteRequestAsync(action);

        if (actionResult.IsSuccess)
        {
            await _router.RouteAsync(this, action.Route ?? actionResult.RoutePath ?? "/", action.Guid, actionResult.Content);
        }
        

        return actionResult;

    }

    public void ExecuteRoute(IAction action)
    {
        _router_context.NavigateTo(_router.MakePath( action.Route ?? "/" , action.Guid));
    }

    public async Task<TModel?> FetchModelAsync<TModel>(IAction? action, string relativePath, object? model = null)
    {

        action = _router.GetSafeAction(action);

        if (action == null)
        {
            return default;
        }

        IExecuteActionResult actionResult = await ExecuteRequestAsync(new HttpMethod(action.Method ?? "get"), _router.MakePath(action.Action, relativePath), model);

        if (actionResult.IsSuccess && actionResult.Content != null)
        {
            return await actionResult.Content.GetContentAsync<TModel>();
        }

        return default;
    }

}
