
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;

namespace Wasm.Kernel.Components;

public class WebForm : EditForm, IAction
{
    [Parameter]
    public string? Action { get; set; }
    [Parameter]
    public string? Method { get; set; }
    [Parameter]
    public string? Route { get; set; }
    [Parameter]
    public Guid? Guid { get; set; }
    [Parameter]
    public EventCallback<EditContext> OnStart { get; set; }
    [Parameter]
    public EventCallback<EditContext> OnDone { get; set; }
    [Parameter]
    public EventCallback<EditContext> OnError { get; set; }


    object? IAction.Model  => EditContext?.Model; 

    protected override void OnParametersSet()
    {

        OnValidSubmit = new EventCallback<EditContext>(null, Submit);
        base.OnParametersSet();
    }

    [Inject] protected IFrontController? Controller { get; set; }


    public async Task Submit()
    {
        if (Controller == null || EditContext == null)
        {
            return;
        }

        if (OnStart.HasDelegate)
        {
            await OnStart.InvokeAsync(EditContext);
        }


        IExecuteActionResult result = await Controller.ExecuteActionAsync(this);


        if (result.IsSuccess && OnDone.HasDelegate)
        {
            await OnDone.InvokeAsync(EditContext);
            return;
        }



        if (OnError.HasDelegate)
        {
            await OnError.InvokeAsync(EditContext);

        }

    }


}
