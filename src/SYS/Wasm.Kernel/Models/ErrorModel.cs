using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;

namespace Wasm.Kernel.Models;

public class ErrorModel : IError
{

    public bool IsWarning { get; set; }

    public string PropertyName { get; set; } = "*";

    public int ErrorStatus { get; set; }

    public string? ErrorCode { get; set; }

    public string? Message { get; set; }

    public ErrorModel() { }


    public ErrorModel(string? reason)
    {
        Message = reason;
    }

    public ErrorModel(IError err) 
    {
        PropertyName = err.PropertyName;
        ErrorCode = err.ErrorCode;
        Message = err.Message;
    }


    public override string ToString() => Message ?? string.Empty;
    

}
