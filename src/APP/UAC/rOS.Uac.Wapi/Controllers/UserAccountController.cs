using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rOS.Security.Api.Accounts;
using rOS.Security.Api.Permissions;
using rOS.Security.Api.Services;
using rOS.Uac.Wapi.Models;

namespace rOS.Uac.Wapi.Controllers;

[Route("user/account")]
[ApiController]
public class UserAccountController : ControllerBase
{
    private readonly IUserAccountService _service;


    public UserAccountController(IUserAccountService userAccountService)
    {
        _service = userAccountService;
    }

    [HttpGet("{userGuid}")]
    public async Task<IActionResult> GetUserAsync(Guid userGuid)
    {
        IUserAccount user = await _service.UserProvider.GetUserAsync(userGuid);

        if (user.IsValid)
        {
            return Ok(new { user.Guid, user.Login, user.Blocked, user.Cellular, user.Email });
        }

        return NotFound();

    }


    [HttpPut("{ownerGuid}/add")]
    public async Task<IActionResult> AddNewUser(Guid? ownerGuid, [FromBody]NewUserModel model)
    {

        model.Login ??= model.Cellular ?? model.Email;

        if (string.IsNullOrWhiteSpace(model.Login))
        {
            return BadRequest();
        }


        if (string.IsNullOrWhiteSpace(model.Password))
        {
            return BadRequest();
        }

        IUserAccount userAccount =  await _service.UserManager.CreateUserAsync(ownerGuid, model.Login ,model.Password);

        if (!userAccount.IsValid)
        {
            return BadRequest();
        }

        if (!string.IsNullOrWhiteSpace(model.Cellular))
        {
            await _service.UserManager.ChangeCellular(userAccount,model.Cellular);
        }


        if (!string.IsNullOrWhiteSpace(model.Email))
        {
            await _service.UserManager.ChangeEmailAsync(userAccount, model.Email);
        }

        
        return Ok(new {userAccount.Guid , userAccount.Login });
    }

    [HttpPut("register")]
    public async Task<IActionResult> RegisterNewUser([FromBody] NewUserModel model) => await AddNewUser(null, model);
   



    [HttpPut("password/{userGuid}")]
    public async Task<IActionResult> ChangePassword(Guid userGuid, [FromBody]ChangePasswordModel model)
    {
        if (model == null)
        {
            return BadRequest();
        }


        if (model.IsValid)
        {
           IUserAccount userAccount = await  _service.UserProvider.GetUserAsync(userGuid);

            if (!userAccount.IsValid)
            {
                return NotFound();
            }

            userAccount = await _service.UserManager.ChangeLoginAsync(userAccount, new UserLoginModel {Name = userAccount.Login , Password = model.NewPassword });

            if (userAccount == null)
            {
                return NotFound();
            }

            return Ok();

        }

        return BadRequest();
    }


    [HttpPut("enable/{userGuid}")]
    public async Task<IActionResult> EnableUser(Guid userGuid)
    {
        IUserAccount userAccount = await _service.UserProvider.GetUserAsync(userGuid);

        if (!userAccount.IsValid)
        {
            return NotFound();
        }


        await _service.UserManager.EnableUserAccountAsync(userAccount);
        return Ok();
    }


    [HttpPut("disable/{userGuid}")]
    public async Task<IActionResult> DisableUser(Guid userGuid)
    {
        IUserAccount userAccount = await _service.UserProvider.GetUserAsync(userGuid);

        if (!userAccount.IsValid)
        {
            return NotFound();
        }


        await _service.UserManager.DisableUserAccountAsync(userAccount);
        return Ok();
    }


    [HttpPut("grant/{userGuid}")]
    public async Task<IActionResult> GrantUserRole([FromRoute] Guid userGuid, [FromBody] UserRoleModel model)
    {

        if (model == null)
        {
            return BadRequest();
        }

        IAccessRole role = await _service.RoleProvider.GetRoleAsync(model.AccessRoleGuid);

        if (role == null)
        {
            return NotFound();
        }

        IUserAccount user = await _service.UserProvider.GetUserAsync(userGuid);

        if (user == null)
        {
            return NotFound();
        }

        user = await _service.UserManager.GrantUserRoleAsync(user, role);

        if (user == null)
        {
            return BadRequest();
        }

        return Ok();
    }



    [HttpPut("revoke/{userGuid}/{model}")]
    public async Task<IActionResult> RevokeUserRole([FromRoute] Guid userGuid, [FromRoute] UserRoleModel model)
    {

        if (model == null)
        {
            return BadRequest();
        }
        
        
        IAccessRole role = await _service.RoleProvider.GetRoleAsync(model.AccessRoleGuid);


        if (role == null)
        {
            return NotFound();
        }

        IUserAccount user = await _service.UserProvider.GetUserAsync(userGuid);

        if (user == null)
        {
            return NotFound();
        }

        user = await _service.UserManager.RevokeUserRoleAsync(user, role);

        if (user == null)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("remove/{userGuid}")]
    public async Task<IActionResult> RemoveUser(Guid userGuid)
    {
        IUserAccount userAccount = await _service.UserProvider.GetUserAsync(userGuid);

        if (!userAccount.IsValid)
        {
            return NotFound();
        }

        if (await _service.UserManager.RemoveUserAsync(userAccount))
        {
            return Ok();
        }

        return NotFound();
    }


}
