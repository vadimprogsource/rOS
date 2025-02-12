using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;

namespace Wasm.Kernel.Providers;

public class MetaDataProvider : IMetaDataProvider
{
    private readonly Dictionary<string, string> m_message = new();


    public IMetaDataProvider AddMessage(string code, string body)
    {
        m_message.Add(code, body);
        return this;
    }

    public string GetMessage(string code)
    {
        if (m_message.TryGetValue(code, out string? message))
        {
            return message ?? code;
        }

        return code;
    }
}
