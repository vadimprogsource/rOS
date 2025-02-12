using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Components;




public class WebListBoxDataSource<T> : IEnumerable<WebListBoxItem>
{

    private readonly HashSet<object?>? m_selected_vals;
    private readonly object? m_selected_val;
    private readonly object? m_data_source;

    public WebListBoxDataSource(object? dataSource, T? currentValue)
    {
        m_data_source = dataSource;

        if (typeof(T).IsArray && currentValue is Array array)
        {

            m_selected_vals = new();

            for (int i = 0; i < array.Length; i++)
            {
                m_selected_vals.Add(array.GetValue(i));
            }

            return;
        }

        m_selected_val = currentValue;

       
    }


    public bool this[object? value]
    {
        get
        {
            if (m_selected_vals is null)
            {

                if (m_selected_val is null && value is null)
                {
                    return true;
                }

                if (m_selected_val is not null || value is not null)
                {
                    return false;
                }

                return m_selected_val != null && m_selected_val.Equals(value);
            }

            return m_selected_vals.Contains(value);
        }
    }



    private IEnumerable<WebListBoxItem> DataSource()
    {

        if (m_data_source is null)
        {
            yield break;
        }


        if (m_data_source is IEnumerable<KeyValuePair<T, string>> @enum)
        {
            foreach (KeyValuePair<T, string> kv in @enum)
            {
                yield return new WebListBoxItem(kv.Key , this[kv.Key], kv.Value);
            }

        }
        else if (m_data_source is Array array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                object? val = array.GetValue(i);
                yield return new WebListBoxItem(val, this[val], val);
            }
               
        }
        else if (m_data_source is IEnumerable en)
        {
            foreach (object val in en)
            {
                yield return new WebListBoxItem(val, this[val], val);
            }
        }
    }


    public IEnumerator<WebListBoxItem> GetEnumerator() => DataSource().GetEnumerator();
    

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


public class WebListBoxItem
{

    public string Value  { get; set; }
    public string Body   { get; set; }
    public bool Selected { get; set; }
   
    public WebListBoxItem(object? value, bool selected, object? body)
    {
        Value = value?.ToString() ?? string.Empty;
        Body  = body?.ToString () ?? string.Empty;
        Selected = selected;
    }


}
