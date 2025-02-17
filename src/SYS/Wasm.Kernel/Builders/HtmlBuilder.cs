using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Builders;


public class HtmlBuilder
{

    private readonly RenderTreeBuilder _tree_builder;
    private readonly object _root_element;
    private int _seq_counter;


    public HtmlBuilder(object @this, RenderTreeBuilder treeBuilder)
    {
        _tree_builder = treeBuilder;
        _seq_counter = 0;
        _root_element = @this;
        Attributes = new ElementAttributes(this);
    }



    protected void OpenTag(string tagName)
    {
        _tree_builder.OpenElement(_seq_counter++, tagName);
    }

    protected void CloseTag()
    {
        _tree_builder.CloseElement();
    }

    private class ElementTag : IDisposable
    {
        private readonly HtmlBuilder m_ui_builder;

        public ElementTag(HtmlBuilder builder, string tagName)
        {
            m_ui_builder = builder;
            m_ui_builder.OpenTag(tagName);
        }

        public void Dispose()
        {
            m_ui_builder.CloseTag();
        }
    }


    public class ElementAttributes
    {
        private readonly HtmlBuilder m_ui_builder;

        public ElementAttributes(HtmlBuilder builder)
        {
            m_ui_builder = builder;
        }


        public string? this[string name] { set => m_ui_builder.AddAttibutes(name, value); }


        public IEnumerable<KeyValuePair<string, object>>? Multiple
        {
            set => m_ui_builder.AddMultipleAttributes(value);
        }

    


        public Func<Task> OnClick { get => throw new NotSupportedException(); set => m_ui_builder.AddEvent("onclick", value,true,true); }
        public EventCallback<ChangeEventArgs> OnChange { get => throw new NotSupportedException(); internal set =>m_ui_builder.AddChangeEvent("onchange", value); }
    }


    private void AddMultipleAttributes(IEnumerable<KeyValuePair<string, object>>? value)
    {
        _tree_builder.AddMultipleAttributes(_seq_counter++, value);
    }

    private void AddAttibutes(string name, string? value)
    {
        _tree_builder.AddAttribute(_seq_counter++, name, value);
    }


    public void AddAttribute<TArgument>(string name, Action<TArgument> callBack)
    {
        _tree_builder.AddAttribute(_seq_counter++, name, EventCallback.Factory.Create(_root_element, callBack));
    }

    public void AddEvent(string eventName, Func<Task> eventCallBack , bool stopPropogation,bool preventDefault)
    {
        _tree_builder.AddAttribute(_seq_counter++, eventName, eventCallBack);
        _tree_builder.AddEventStopPropagationAttribute (_seq_counter++, eventName, stopPropogation);
        _tree_builder.AddEventPreventDefaultAttribute  (_seq_counter++ , eventName, preventDefault );
    }


    public void AddChangeEvent(string name, EventCallback<ChangeEventArgs> callback)
    {
        _tree_builder.AddAttribute(_seq_counter++, name, callback);
    }



    public IDisposable this[string tagName]
    {
        get
        {
            return new ElementTag(this, tagName);
        }
    }

    public string? InnerHtml { set => _tree_builder.AddMarkupContent(_seq_counter++, value); }
    public string? InnerText { set => _tree_builder.AddContent(_seq_counter++, value); }
    public RenderFragment? Content { set => _tree_builder.AddContent(_seq_counter++, value); }
    public ElementAttributes Attributes { get; private set; }
    public Action<ElementReference> Reference { set => _tree_builder.AddElementReferenceCapture(_seq_counter++, value); }


}
