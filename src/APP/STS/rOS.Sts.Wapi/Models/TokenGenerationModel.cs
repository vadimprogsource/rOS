using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rOS.Security.Api;
using rOS.Security.Api.Tokens;

namespace rOS.Sts.Wapi.Models;

public class TokenGenerationModel: ISecurityTokenRequest 
{
    public string    Owner             { get; set; } = string.Empty;
    public string    Role              { get; set; } ="Guest";
    public string    TypeCode          { get; set; } = "A";
    public long      ExpiredInSeconds  { get; set; } = 20;

    public string Token { get; set; } = string.Empty;
}
