using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wasm.Kernel.Components;

public class WebListBox<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : WebControl<T>
{
    protected override bool IsListBox => true;

}
