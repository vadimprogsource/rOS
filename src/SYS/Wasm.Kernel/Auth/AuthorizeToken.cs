using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;

namespace Wasm.Kernel.Api;

public class AuthorizeToken : IAuthorizeToken
{
    public bool IsAuthorized => !string.IsNullOrWhiteSpace(Token);

    public string? Token { get; private set; } 



    public AuthorizeToken() { }
    public AuthorizeToken(IExecuteActionResult actionResult) 
    {
        Token = actionResult.AuthorizeToken;
    }


    public AuthorizeToken(string? s)
    {
        Token = s;
    }

    public void Intialize(IExecuteActionResult actionResult)
    {
        Token = actionResult.IsAuth ? actionResult.AuthorizeToken : null;
    }
}
