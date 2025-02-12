using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;
using Wasm.Kernel.Builders;

namespace Wasm.Kernel.Components;

public abstract class WebComponent : ComponentBase, IAction
{
    [Parameter]
    public RenderFragment? ChildContent
    {
        get;
        set;
    }

    public ElementReference? Element
    {
        get;
        protected set;
    }

    [Parameter]
    public string? Action
    {
        get;
        set;
    }

    [Parameter]
    public string? Method
    {
        get;
        set;
    }

    [Parameter]
    public string? Route
    {
        get;
        set;
    }

    [Parameter]
    public Guid? Guid { get; set; }


    [Parameter]
    public bool Disabled { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes
    {
        get;
        set;
    }

    [Parameter]
    public bool Visible { get; set; } = true;


    public abstract string TagName { get; }
    public virtual object? Model { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Visible)
        {
            RenderElement(new HtmlBuilder(this, builder));
        }
    }


    protected virtual void DefineReference(HtmlBuilder builder)
    {
        builder.Reference = x => Element = x;

    }

    protected virtual void RenderElement(HtmlBuilder builder)
    {
        using (builder[TagName])
        {

            RenderAttributes(builder, AdditionalAttributes);
            DefineReference(builder);
            RenderContent(builder, ChildContent);
        }
    }

    protected virtual void RenderAttributes(HtmlBuilder builder, IReadOnlyDictionary<string, object>? attr)
    {
        builder.Attributes.Multiple = attr;
    }

    protected virtual void RenderContent(HtmlBuilder builder, RenderFragment? content)
    {
        builder.Content = content;
    }


    protected void RenderAttributes(HtmlBuilder builder, IReadOnlyDictionary<string, object>? attr, IReadOnlyDictionary<string, object> defs)
    {
        if (attr == null || attr.Count < 1)
        {
            builder.Attributes.Multiple = defs;
            return;
        }


        builder.Attributes.Multiple = defs.Where(x => !attr.ContainsKey(x.Key));
        builder.Attributes.Multiple = attr.Where(x => !defs.ContainsKey(x.Key));
    }


    protected string GetCssClass(string? requiredClass = null, string? defaultClass = null)
    {
        StringBuilder cssBuilder = new();

        if (!string.IsNullOrWhiteSpace(requiredClass))
        {
            cssBuilder.Append(requiredClass);
        }

        if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("class", out object? css) && css!=null)
        {
            if (cssBuilder.Length > 0)
            {
                cssBuilder.Append(' ');
            }

            cssBuilder.Append(css);
        }

        if (string.IsNullOrWhiteSpace(defaultClass))
        {
            return cssBuilder.ToString();
        }

        if (cssBuilder.Length < 1)
        {
            cssBuilder.Append(defaultClass);
        }

        return cssBuilder.ToString();
    }


}
