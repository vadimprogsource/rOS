using System;
using rOS.Core.Api;
using rOS.Security.Api.Tokens;

namespace rOS.Sts.Wapi.Models;

public class TokenOwnerModel : ISecurityTokenOwnerRequest
{


    public string? Role { get; set; } 

    public string? Owner { get; set; }

    public string? TypeCode { get; set; }


    string ISecurityTokenOwnerRequest.Role => Role ?? string.Empty;
    string ISecurityTokenOwnerRequest.Owner => Owner ?? string.Empty;
    string ITyped.TypeCode => TypeCode ?? string.Empty;


}

