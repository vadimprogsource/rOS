using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;
using Wasm.Kernel.Builders;

namespace Wasm.Kernel.Components;

public  class WebControl<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>:WebComponent
{


    [CascadingParameter]
    public EditContext? EditContext { get; set; }

    [Parameter]
    public T? Value { get; set; }

    [Parameter]
    public EventCallback<T> ValueChanged { get; set; }

    [Parameter]
    public Expression<Func<T>>? ValueExpression { get; set; }

    [Parameter]
    public bool Password { get; set; }

    [Parameter]
    public int? Rows { get; set; }


    [Inject]
    public IErrorProvider? ErrorProvider { get; set; } 

    public string PropertyName
    {
        get
        {
            if (ValueExpression?.Body is MemberExpression m)
            {
                return m.Member.Name;
            }

            return string.Empty;
        }
    }

    private IError? m_error;
    private FieldIdentifier m_field_identifier;




    [Parameter]
    public bool Multiline { get; set; }




  
    


    [Parameter]
    public string? ContainerCssClass { get; set; }

    [Parameter]
    public string? LabelCssClass { get; set; }
    [Parameter]
    public string? ControlCssClass { get; set; }


    [Parameter]
    public object? DataSource { get; set; }

    [Parameter]
    public bool Multiple { get; set; }

    [Parameter]
    public bool Floating { get; set; } = false;

    [Parameter]
    public bool Large { get; set; } = false;


    protected override void OnParametersSet()
    {

        if (IsCheckBox)
        {
            ContainerCssClass ??= "form-check";
            LabelCssClass ??= "form-check-label";
        }
        else
        {
            ContainerCssClass ??= "row";
            LabelCssClass ??= "form-label";
        }




        m_error = null;

        if (ErrorProvider != null && ErrorProvider.HasErrors)
        {
            m_error = ErrorProvider?.GetError(PropertyName);
        }

        if (ValueExpression != null)
        {
            m_field_identifier = FieldIdentifier.Create(ValueExpression);
        }

        base.OnParametersSet();
    }

    protected string GetFormControlCss(string? @class=null)
    {
        StringBuilder css = new("form-control");

        if (Large)
        {
            css.Append(" form-control-lg");
        }

        if (@class != null)
        {
            css.Append(' ').Append(@class);
        }

        return css.ToString();
    }


    protected override void RenderElement(HtmlBuilder builder)
    {

        if (Floating)
        {
            using (builder["div"])
            {
                builder.Attributes["class"] = GetCssClass( "form-floating");
                RenderControl(builder, GetFormControlCss());
                RenderLabel(builder, null);
            }

            return;
        }


        if (ChildContent == null)
        {
            RenderControl(builder , GetFormControlCss());
            return;
        }


        using (builder["div"])
        {

            builder.Attributes["class"] = ContainerCssClass;

            RenderLabel(builder, LabelCssClass);


            if (string.IsNullOrWhiteSpace(ControlCssClass))
            {
                RenderControl(builder, GetFormControlCss());
            }
            else
            {
                using (builder["div"])
                {
                    builder.Attributes["class"] = ControlCssClass;
                    RenderControl(builder, GetFormControlCss());
                }
            }

        }

    }



    protected virtual void RenderLabel(HtmlBuilder builder, string? cssClass)
    {
        using (builder["label"])
        {
            builder.Attributes["for"] = PropertyName;
            builder.Attributes["class"] = cssClass;



            if (m_error != null)
            {
                builder.Attributes["style"] = "color:red";
                builder.Attributes["title"] = m_error.Message;

                if (Floating)
                {
                  builder.Content = builder => builder.AddContent(1, m_error.Message);
                  return;
                }

            }

            builder.Content = ChildContent;
        }

    }


    protected virtual void RenderControlContent(HtmlBuilder builder)
    {


        WebListBoxDataSource<T> dataSource = new (DataSource, Value);


        if (dataSource.Any())
        {
            foreach (WebListBoxItem item in dataSource)
            {
                using (builder["option"])
                {
                    builder.Attributes["value"] = item.Value;

                    if (item.Selected)
                    {
                        builder.Attributes["selected"] = "selected";
                    }

                    builder.InnerHtml = item.Body;

                }
            }

        }


    }


    protected virtual bool IsListBox => DataSource is not null;
    protected static bool IsCheckBox => typeof(T) == typeof(bool) || typeof(T) == typeof(bool?);


    protected virtual void RenderControl(HtmlBuilder builder, string? cssClass)
    {

        using (builder[TagName])
        {

            string? type = null;

            if (AdditionalAttributes != null)
            {

                if (AdditionalAttributes.TryGetValue("type", out object? obj) && obj != null)
                {
                    if (string.IsNullOrWhiteSpace(type = obj.ToString())) type = null;
                }


                builder.Attributes.Multiple = AdditionalAttributes.Where(x => x.Key != "class" && x.Key != "type" && x.Key != "id" && x.Key != "name" && x.Key != "style" && x.Key != "title");
                
            }



            builder.Attributes["id"] = PropertyName;
            builder.Attributes["name"] = PropertyName;


            string? requiredCss = null;

            if (m_error != null)
            {
                builder.Attributes["title"] = m_error.Message;
                requiredCss = GetFormControlCss( "is-invalid");
            }
            else
            {
                if (EditContext != null && EditContext.IsModified(m_field_identifier))
                {
                    requiredCss = GetFormControlCss("is-valid");
                }
                
            }

        

            builder.Attributes.OnChange = EventCallback.Factory.CreateBinder<T?>(this, x => SetValue(x), Value,null);


            if (IsCheckBox)
            {
                builder.Attributes["class"] = GetCssClass(string.Empty, "form-check-input");

                builder.Attributes["type"] = type ?? "checkbox";

                if (true.Equals(Value))
                {
                    builder.Attributes["checked"] = "checked";
                }

                builder.Attributes["value"] = bool.TrueString;
                return;

            }
            
            
            if (IsListBox)
            {
                if (Multiple || typeof(T).IsArray)
                {
                    builder.Attributes["multiple"] = "multiple";
                }

                builder.Attributes["class"] = GetCssClass(requiredCss, "form-select");


                foreach (WebListBoxItem item in new WebListBoxDataSource<T>(DataSource , Value))
                {
                    using (builder["option"])
                    {
                        builder.Attributes["value"] = item.Value;

                        if (item.Selected)
                        {
                            builder.Attributes["selected"] = "selected";
                        }

                        builder.InnerText = item.Body;

                    }
                }

                return;

            }

            builder.Attributes["class"] = GetCssClass(requiredCss,GetFormControlCss());

            if (Multiline)
            {
                if (Rows.HasValue && Rows > 0)
                {
                    builder.Attributes["rows"] = Rows.ToString();
                }

                builder.InnerText = GetValue();
                return;
            }
            else
            {
                builder.Attributes["value"] = GetValue();
            }



            builder.Attributes["type"]  = type ?? ( Password ? "password" : "input");
           

        }

    }




    public override string TagName
    {
        get
        {

            if (IsListBox)
            {
                return "select";
            }

            return Multiline ? "textarea" : "input";
        }
    }


    public virtual string? GetValue()=> Value?.ToString();
    public virtual void SetValue(T? value)
    {

        if (value == null && this.Value == null)
        {
            return;
        }

        EqualityComparer<T>? comparer = EqualityComparer<T>.Default;

        if (comparer == null)
        {
            if (this.Value != null && this.Value.Equals(value))
            {
                return;
            }

            if (value != null && value.Equals(this.Value))
            {
                return;
            }

        }
        else
        {
          if (comparer.Equals(value, this.Value)) return;
        }

        _ = ValueChanged.InvokeAsync(value);
        EditContext?.NotifyFieldChanged(m_field_identifier);
    }




    protected override void DefineReference(HtmlBuilder builder)
    {
    }




}
