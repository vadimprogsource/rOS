using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasm.Kernel.Models;

public class ErrorContentModel
{
    public bool Success { get; set; }
    public bool HasErrors { get; set; }
    public ErrorModel[]? Errors { get; set; }
}
