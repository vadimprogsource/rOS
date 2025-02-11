using System.CoreLib.Dynamics;
using Microsoft.AspNetCore.Mvc;
using rOS.Security.Api.Services;
using rOS.Security.Api.Tokens;

namespace rOS.Sts.Wapi.Controllers;

[Route("session")]
[ApiController]
public class SessionController : ControllerBase
{
    private readonly IIdentityService m_service;

    public SessionController(IIdentityService service)
    {
        m_service = service;
    }

    [HttpGet("data/{token}")]
    public async Task<IActionResult> GetSessionData(string token)
    {
        ISecurityToken securityToken = await m_service.TokenProvider.GetTokenInfoAsync(token);

      

        if (securityToken.IsValid)
        {
            IDictionary<string,object> session = await m_service.SessionStorage.GetDataAsync(securityToken);

            if (session.Count <1)
            {
                return NoContent();
            }

            DynamicModel model = DynamicModel.CreateModel(session, x => x.Key, x => x.Value);
            return Ok(model);
        }

        return NotFound();
    }


    [HttpPut("data/{token}")]
    public async Task<IActionResult> PutSessionData(string token,[FromBody]DynamicModel model)
    {
        ISecurityToken securityToken = await m_service.TokenProvider.GetTokenInfoAsync(token);

        if (securityToken == null)
        {
            return NotFound();
        }


        if (securityToken.IsValid)
        {

            if (model.IsEmpty)
            {
                return NoContent();
            }


            await m_service.SessionStorage.PutDataAsync(securityToken, model.ToDictionary());
            return Ok();
        }


        return BadRequest();

    }
}
