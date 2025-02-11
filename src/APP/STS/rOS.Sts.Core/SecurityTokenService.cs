using System;
using rOS.Security.Api.Services;
using rOS.Security.Api.Tokens;
using rOS.Security.Entity;

namespace rOS.Sts.Core;

public class SecurityTokenService : ISecurityTokenService
{
    private readonly IIdentityService _indentity_service;

    public SecurityTokenService(IIdentityService service)
    {
        _indentity_service = service;
    }

    public async Task<ISecurityToken> GenerateTokenAsync(ISecurityTokenOwnerRequest request)
    {
        ISecurityTokenOwner securityTokenOwner = await _indentity_service.TokenProvider.GetTokenOwnerAsync(request.Owner, request.Role);


        ISecurityToken securityToken = await _indentity_service.TokenManager.GenerateNewTokenAsync(securityTokenOwner, request.TypeCode, _indentity_service.Config.DefaultTokenTimeout);

        if (securityToken.IsValid)
        {
            return securityToken;
        }

        return SecurityToken.Empty;
    }


    public async Task<ISecurityRefreshToken> GenerateRefreshTokenAsync(ISecurityTokenOwnerRequest request)
    {
        ISecurityTokenOwner securityTokenOwner = await _indentity_service.TokenProvider.GetTokenOwnerAsync(request.Owner, request.Role);

        ISecurityToken refreshToken = await _indentity_service.TokenManager.GenerateNewTokenAsync(securityTokenOwner, "R", _indentity_service.Config.RefreshTokenTimeout);

        if (refreshToken.IsValid)
        {
            ISecurityToken accessToken = await _indentity_service.TokenManager.GenerateNewTokenAsync(refreshToken, "A", _indentity_service.Config.AccessTokenTimeout);

            if (accessToken.IsValid)
            {
                return new SecurityRefreshToken(refreshToken, accessToken);
            }
        }

        return SecurityRefreshToken.Empty;
    }





    public async Task<ISecurityRefreshToken> GenerateAccessTokenAsync(ISecurityTokenRequest request)
    {

        ISecurityToken refreshToken = await _indentity_service.TokenProvider.GetTokenInfoAsync(request.Token);

        if (refreshToken.IsValid)
        {
            refreshToken = await _indentity_service.TokenManager.GenerateNewTokenAsync(refreshToken, "R", _indentity_service.Config.RefreshTokenTimeout);

            if (refreshToken.IsValid)
            {
                ISecurityToken accessToken = await _indentity_service.TokenManager.GenerateNewTokenAsync(refreshToken, "A", _indentity_service.Config.AccessTokenTimeout);

                if (accessToken.IsValid)
                {
                    return new SecurityRefreshToken(refreshToken, accessToken);
                }
            }
        }

        return SecurityRefreshToken.Empty;
    }


    public async Task<ISecurityTokenInfo> GetTokenInfo(ISecurityTokenRequest request)
    {
        ISecurityToken securityToken = await _indentity_service.TokenProvider.GetTokenInfoAsync(request.Token);


        if (securityToken.IsValid)
        {

            if (request.TypeCode == securityToken.TypeCode)
            {
                return new SecurityTokenInfo(securityToken);
            }

            await _indentity_service.TokenManager.ReleaseTokenAsync(securityToken);

        }

        return new SecurityTokenInfo(SecurityToken.Empty);
    }

    public async Task<bool> IsValidAsync(ISecurityTokenRequest request)
    {
        ISecurityToken securityToken = await _indentity_service.TokenProvider.GetTokenInfoAsync(request.Token);
        return securityToken.IsValid;
    }

    public async Task ReleaseTokenAsync(ISecurityTokenRequest request)
    {
        ISecurityToken securityToken = await _indentity_service.TokenProvider.GetTokenInfoAsync(request.Token);

        if (securityToken.IsValid)
        {
            await _indentity_service.TokenManager.ReleaseTokenAsync(securityToken);
        }
    }

    public async Task ReleaseTokenForOwnerAsync(ISecurityTokenOwnerRequest request)
    {
        ISecurityTokenOwner tokenOwner = await _indentity_service.TokenProvider.GetTokenOwnerAsync(request.Owner, request.Role);
        await _indentity_service.TokenManager.ReleaseTokensForOwnerAsync(tokenOwner);
    }






}

