using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface ILocalStorage
    {
        Task<string?> GetAsync(string name);
        Task PutAsync(string name, string? value);
        Task DeleteAsync(string name);
    }

