using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;

namespace Wasm.Kernel.Providers;

public class MetaDataProvider : IMetaDataProvider
{
    private readonly Dictionary<string, string> _message = new();


    public IMetaDataProvider AddMessage(string code, string body)
    {
        _message.Add(code, body);
        return this;
    }

    public string GetMessage(string code)
    {
        if (_message.TryGetValue(code, out string? message))
        {
            return message ?? code;
        }

        return code;
    }
}
