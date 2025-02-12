using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Builders;

namespace Wasm.Kernel.Components;

public class WebWait : WebComponent
{
    public override string TagName => "div";

    [Parameter]
    public bool InProcess { get=>Visible; set=>Visible = value; }

    protected override void RenderAttributes(HtmlBuilder builder, IReadOnlyDictionary<string, object>? attr)
    {
        RenderAttributes(builder, attr, new Dictionary<string, object> { { "class", "spinner-border" }, { "role", "status" } });
    }

    protected override void RenderElement(HtmlBuilder builder)
    {
        if (InProcess)
        {
            base.RenderElement(builder);
        }
    }

    protected override void RenderContent(HtmlBuilder builder, RenderFragment? content)
    {
        using (builder["span"])
        {
            builder.Attributes["class"] = "sr-only";
            builder.Content = content;
        }
    }
}
