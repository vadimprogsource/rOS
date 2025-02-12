using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;

namespace Wasm.Kernel.Models;

public class ActonModel : IAction
{
    public Guid? Guid {get;set;}

    public string? Action { get; set; }

    public string? Method { get; set; }

    public string? Route { get; set; }

    public object? Model { get; set; }

    public ActonModel(string route)
    {
        Route = route;
    }

    public ActonModel() { }
}
