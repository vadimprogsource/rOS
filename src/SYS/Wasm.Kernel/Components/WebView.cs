using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;

namespace Wasm.Kernel.Components;

public class WebView<TModel> : ComponentBase, IAction where TModel : class, new()
{
    protected TModel Model { get; set; } = new();


    [Parameter]
    public string? Action { get; set; } = string.Empty;
    [Parameter]
    public string? Method { get; set; } = string.Empty;
    [Parameter]
    public Guid? Guid { get; set; }

    [Inject] protected IFrontController? Controller { get; set; } = default;
    [Inject] protected IErrorProvider? ErrorProvider { get; set; } = default;
    public string? Route { get => null; set { } }
    object? IAction.Model => null;

    protected async override Task OnInitializedAsync()
    {
        if (Controller == null)
        {
            Model = new();
            return;
        }

        if (ErrorProvider != null)
        {
            ErrorProvider.Success();
        }

        Model = await Controller.GetModelAsync<TModel>(this) ?? new();
    }


    private void ResetModel(TModel? model)
    {
        if (model != null)
        {
            Model = model;
            StateHasChanged();
        }
    }


    protected async Task RefreshModel(HttpMethod method ,  string path,object? content=null)
    {
        if (Controller != null)
        {
            ResetModel(await Controller.FetchModelAsync<TModel>(method, path, content));
        }
    }


    protected async Task Pagination(int pageIndex, int pageSize)
    {
        if (Controller != null)
        {
            ResetModel(await Controller.FetchModelAsync<TModel>(this, $"{pageIndex}/{pageSize}"));
        }

       
     
    }


    public bool HasErrors => ErrorProvider ==null || ErrorProvider.HasErrors;

    public bool HasSystemErrors => ErrorProvider!=null && ErrorProvider.HasErrors && SystemErrors.Any();

    public bool IsError<TValue>(Expression<Func<TModel, TValue>> propertyOrField)
    {
        if (ErrorProvider != null &&  propertyOrField.Body is MemberExpression m)
        {
            return ErrorProvider.IsError(m.Member.Name);
        }

        return true;

    }


    public IError? GetError<TValue>(Expression<Func<TModel, TValue>> propertyOrField)
    {
        if (ErrorProvider != null && propertyOrField.Body is MemberExpression m)
        {
            return ErrorProvider.GetError(m.Member.Name);
        }

        return default;
    }

    public IError[] Errors => ErrorProvider?.GetErrors() ?? Array.Empty<IError>();
    public IEnumerable<IError> SystemErrors => Errors.Where(x => string.IsNullOrWhiteSpace(x.PropertyName) || x.PropertyName =="*");
}
