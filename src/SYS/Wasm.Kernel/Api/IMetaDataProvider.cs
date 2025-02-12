using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Api;

    public interface IMetaDataProvider
    {
        IMetaDataProvider AddMessage  (string code, string body);
        string GetMessage(string code);
    }

