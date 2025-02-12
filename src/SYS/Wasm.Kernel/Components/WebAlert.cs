using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;
using Wasm.Kernel.Builders;

namespace Wasm.Kernel.Components;

public class WebAlert : WebComponent
{
    public override string TagName => "div";
    [Parameter]
    public IEnumerable<object>? DataSource { get; set; }
 






    protected override void RenderAttributes(HtmlBuilder builder, IReadOnlyDictionary<string, object>? attr)
    {
        RenderAttributes(builder, attr, new Dictionary<string, object> { { "role", "alert" }, { "class", "alert alert-danger" } });
    }

    protected override void RenderContent(HtmlBuilder builder, RenderFragment? content)
    {
            StringBuilder sb = new();

            foreach (object item in DataSource??Enumerable.Empty<object>())
            {
                if (sb.Length > 0)
                {
                    sb.Append("<br/>");
                }

                sb.Append(item);
            }


        if (sb.Length < 1)
        {
            builder.Content = content;
            return;
        }

        builder.InnerHtml = sb.ToString();



      
    }


}
