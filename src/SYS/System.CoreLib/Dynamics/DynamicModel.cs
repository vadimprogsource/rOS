using System;
using System.Dynamic;
using System.Text.Json.Serialization;

namespace System.CoreLib.Dynamics;


[JsonConverter(typeof(DynamicModelConverter))]
public class DynamicModel : DynamicObject
{
    private readonly Dictionary<string, object> _properties = new ();



    public DynamicModel() { }



    public object? this[string propertyName]
    {
        get
        {
            if (_properties.TryGetValue(propertyName, out object? val))
            {
                return val;
            }

            return default;
        }

        set
        {
            if (value == null)
            {
                _properties.Remove(propertyName);
                return;
            }


            _properties[propertyName] = value;
        }
    }

    public override IEnumerable<string> GetDynamicMemberNames() => _properties.Values.OrderBy(x => x).OfType<string>();


    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        return _properties.TryGetValue(binder.Name, out result);
    }


    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        this[binder.Name] = value;
        return true;
    }


    public static DynamicModel CreateModel<T>(IEnumerable<T> properies, Func<T, string> getName, Func<T, object> getValue)
    {
        DynamicModel model = new DynamicModel();

        foreach (T prop in properies)
        {
            model[getName(prop)] = getValue(prop);
        }

        return model;
    }


    public bool IsEmpty => _properties.Count == 0;


    public dynamic ToDynamic() => this;
    public IDictionary<string, object> ToDictionary() => _properties;



}


