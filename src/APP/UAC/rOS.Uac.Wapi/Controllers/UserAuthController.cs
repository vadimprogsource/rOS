using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rOS.Security.Api;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Providers;
using rOS.Uac.Wapi.Models;

namespace rOS.Uac.Wapi.Controllers;

[Route("user/auth")]
[ApiController]
public class UserAuthController : ControllerBase
{
    private IUserAuthProvider _provider;

    public UserAuthController(IUserAuthProvider provider)
    {
        _provider = provider;
    }


    [HttpGet("userGuid")]
    public async Task<IActionResult> GetLoggerUser(Guid userGuid)
    {
        IUserAccount logged = await _provider.GetActiveUserAsync(userGuid);

        if (logged == null)
        {
            return NotFound();
        }

        return Ok(new { logged.Guid, logged.Login });
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody]UserLoginModel model)
    {
        IUserAccount logged = await _provider.GetLoginAsync(model);

        if (logged == null || !logged.IsValid)
        {
            return NotFound();
        }

        return Ok(new { logged.Guid, logged.Login });
    }

}
