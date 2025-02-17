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

    private readonly IJSRuntime _java_script;

    public LocalStorage(IJSRuntime js)
    {
        _java_script = js;
    }


    public async Task<string?> GetAsync(string name)
    {
        return await _java_script.InvokeAsync<string>("localStorage.getItem", name);
    }

    public async Task PutAsync(string name, string? value)
    {

        if (string.IsNullOrWhiteSpace(value))
        {
            await DeleteAsync(name);
        }

        await _java_script.InvokeVoidAsync("localStorage.setItem",name, value);
    }

    public async Task DeleteAsync(string name)
    {
        await _java_script.InvokeVoidAsync("localStorage.removeItem", name);
    }

}
