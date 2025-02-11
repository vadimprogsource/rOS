using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using rOS.Security.Api;
using rOS.Security.Api.Services;
using rOS.Security.Api.Tokens;
using rOS.Sts.Wapi.Models;

namespace rOS.Sts.Wapi.Controllers;

[Route("token")]
[ApiController]
public class TokenController : ControllerBase 
{
    private readonly ISecurityTokenService m_service;

    public TokenController(ISecurityTokenService service)
    {
        m_service = service;
    }



    [HttpGet("info/{typeCode}/{token}")]
    public async Task<IActionResult> GetTokenAsync(string typeCode , string token)
    {
        ISecurityTokenInfo securityToken = await m_service.GetTokenInfo(new TokenModel {TypeCode = typeCode , Token = token });

        if (securityToken.IsValid)
        {
            return Ok(securityToken);
        }

        return NotFound();
    }


    [HttpGet("valid/{token}")]
    public async Task<IActionResult> IsValidAsync(string token)
    {
        bool securityTokenFlag = await m_service.IsValidAsync(new TokenModel { Token = token });

        if (securityTokenFlag)
        {
            return Ok();
        }

        return NotFound();
    }



    [HttpPost("generate")]
    public async Task<IActionResult> GenerateTokenAsync([FromBody]TokenOwnerModel model)
    {
        ISecurityToken securityToken = await m_service.GenerateTokenAsync(model);

        if (securityToken.IsValid)
        {
           return Ok(new TokenModel(securityToken));
        }
      
        return BadRequest();
    }


    [HttpPost("generate-refresh")]
    public async Task<IActionResult> GenerateRefreshTokenAsync([FromBody]TokenOwnerModel model)
    {
        ISecurityRefreshToken token = await m_service.GenerateRefreshTokenAsync(model);

        if (token.IsValid)
        {
            return Ok(token);
        }
     
        return BadRequest();

    }


    [HttpPost("generate-access/{token}")]
    public async Task<IActionResult> GenerateAccessTokenAsync([FromBody]TokenModel model)
    {
        ISecurityRefreshToken refreshToken = await m_service.GenerateAccessTokenAsync(model);

        if (refreshToken.IsValid)
        {
            return Ok(new {Refresh =  refreshToken.RefreshToken,Access =  refreshToken.AccessToken });
        }

        return BadRequest();

    }



    [HttpDelete("release/{token}")]
    public async Task<IActionResult> ReleaseTokenAsync(string token)
    {
        await m_service.ReleaseTokenAsync(new TokenGenerationModel{Token = token });
        return NoContent();
    }

    [HttpDelete("release/all/{owner}/{role}")]
    public async Task<IActionResult> ReleaseTokensForOwner(string owner,string role)
    {
        await m_service.ReleaseTokenForOwnerAsync(new TokenOwnerModel{ Owner = owner ,Role = role } );
        return NoContent();
    }



}
