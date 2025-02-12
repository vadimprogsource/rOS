using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;
using Wasm.Kernel.Builders;

namespace Wasm.Kernel.Components;



public class WebLink : WebComponent
{
    public override string TagName => "a";

    [Inject] public IFrontController? Controller { get; set; }



    private string? GetDefaultRef()
    {

        if (string.IsNullOrWhiteSpace(Route))
        {
            return "javascript:void";
        }

        if (Guid is not null)
        {
            return $"{Route}/{Guid:n}";
        }

        return Route;
    }


    protected override void RenderAttributes(HtmlBuilder builder, IReadOnlyDictionary<string, object>? attr)
    {

        if (Disabled)
        {
            builder.Attributes["href"] = "javascript:void";

            if (attr != null)
            {
                builder.Attributes.Multiple = attr.Where(x => x.Key != "href");
            }

            return;
        }



        if (attr == null)
        {
            builder.Attributes["href"] = GetDefaultRef();
            return;
        }

        if (string.IsNullOrWhiteSpace(Route) && attr.TryGetValue("href", out object? v))
        {
            Route = v?.ToString();
        }

        builder.Attributes["href"] = GetDefaultRef();


        builder.Attributes.Multiple = attr.Where(x => x.Key != "href");

        builder.Attributes.OnClick = OnClick;
        
    }

    public virtual async Task OnClick()
    {
        if (Controller != null)
        {
            _ = await Controller.ExecuteActionAsync(this);
        }

    }
}
