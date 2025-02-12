using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;

namespace Wasm.Kernel.Storages;

public class LocalStorage : ILocalStorage
{

    private readonly IJSRuntime m_java_script;

    public LocalStorage(IJSRuntime js)
    {
        m_java_script = js;
    }


    public async Task<string?> GetAsync(string name)
    {
        return await m_java_script.InvokeAsync<string>("localStorage.getItem", name);
    }

    public async Task PutAsync(string name, string? value)
    {

        if (string.IsNullOrWhiteSpace(value))
        {
            await DeleteAsync(name);
        }

        await m_java_script.InvokeVoidAsync("localStorage.setItem",name, value);
    }

    public async Task DeleteAsync(string name)
    {
        await m_java_script.InvokeVoidAsync("localStorage.removeItem", name);
    }

}
