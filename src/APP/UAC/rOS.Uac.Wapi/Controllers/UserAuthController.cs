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
    private IUserAuthProvider m_provider;

    public UserAuthController(IUserAuthProvider provider)
    {
        m_provider = provider;
    }


    [HttpGet("userGuid")]
    public async Task<IActionResult> GetLoggerUser(Guid userGuid)
    {
        IUserAccount logged = await m_provider.GetActiveUserAsync(userGuid);

        if (logged == null)
        {
            return NotFound();
        }

        return Ok(new { logged.Guid, logged.Login });
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody]UserLoginModel model)
    {
        IUserAccount logged = await m_provider.GetLoginAsync(model);

        if (logged == null || !logged.IsValid)
        {
            return NotFound();
        }

        return Ok(new { logged.Guid, logged.Login });
    }

}
