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

    private readonly RenderTreeBuilder m_tree_builder;
    private readonly object m_root_element;
    private int m_seq_counter;


    public HtmlBuilder(object @this, RenderTreeBuilder treeBuilder)
    {
        m_tree_builder = treeBuilder;
        m_seq_counter = 0;
        m_root_element = @this;
        Attributes = new ElementAttributes(this);
    }



    protected void OpenTag(string tagName)
    {
        m_tree_builder.OpenElement(m_seq_counter++, tagName);
    }

    protected void CloseTag()
    {
        m_tree_builder.CloseElement();
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
        m_tree_builder.AddMultipleAttributes(m_seq_counter++, value);
    }

    private void AddAttibutes(string name, string? value)
    {
        m_tree_builder.AddAttribute(m_seq_counter++, name, value);
    }


    public void AddAttribute<TArgument>(string name, Action<TArgument> callBack)
    {
        m_tree_builder.AddAttribute(m_seq_counter++, name, EventCallback.Factory.Create(m_root_element, callBack));
    }

    public void AddEvent(string eventName, Func<Task> eventCallBack , bool stopPropogation,bool preventDefault)
    {
        m_tree_builder.AddAttribute(m_seq_counter++, eventName, eventCallBack);
        m_tree_builder.AddEventStopPropagationAttribute (m_seq_counter++, eventName, stopPropogation);
        m_tree_builder.AddEventPreventDefaultAttribute  (m_seq_counter++ , eventName, preventDefault );
    }


    public void AddChangeEvent(string name, EventCallback<ChangeEventArgs> callback)
    {
        m_tree_builder.AddAttribute(m_seq_counter++, name, callback);
    }



    public IDisposable this[string tagName]
    {
        get
        {
            return new ElementTag(this, tagName);
        }
    }

    public string? InnerHtml { set => m_tree_builder.AddMarkupContent(m_seq_counter++, value); }
    public string? InnerText { set => m_tree_builder.AddContent(m_seq_counter++, value); }
    public RenderFragment? Content { set => m_tree_builder.AddContent(m_seq_counter++, value); }
    public ElementAttributes Attributes { get; private set; }
    public Action<ElementReference> Reference { set => m_tree_builder.AddElementReferenceCapture(m_seq_counter++, value); }


}
