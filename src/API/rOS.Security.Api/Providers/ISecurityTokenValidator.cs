using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace rOS.Security.Api.Providers;

public interface ISecurityTokenValidator
{
    Task<bool> IsValidAsync(string token);
}
