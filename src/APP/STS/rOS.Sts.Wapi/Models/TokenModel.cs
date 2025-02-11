using System;
using rOS.Core.Api;
using rOS.Security.Api.Tokens;

namespace rOS.Sts.Wapi.Models;

public class TokenModel : ISecurityTokenRequest
{

    public string? Token { get; set; }

    public string? TypeCode { get; set; }

    string ISecurityTokenRequest.Token=> Token ?? string.Empty;
    string ITyped.TypeCode => TypeCode ?? string.Empty;


    public TokenModel() { }
    public TokenModel(ISecurityToken token)
    {
        Token = token.ToString();
        TypeCode = token.TypeCode;
    }

}

