using rOS.Security.Api.Accounts;
using rOS.Security.Api.Permissions;
using rOS.Security.Entity.Accounts;

namespace rOS.Uac.InMemory;

internal class DataUserAccount : UserAccount
{

    public static readonly IUserAccount Empty = new DataUserAccount { Blocked = true,CreatedOn = DateTime.MinValue,Guid = Guid.Empty };



    public override Guid Guid { get; set; }

    public override Guid PasswordGuid { get; set; } = Guid.Empty;
    public override byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public override string Login { get; set; } = string.Empty;
    public override bool Blocked { get; set; }
    public override string? Cellular { get; set; }
    public override string? Email { get; set; }
    public override DateTime CreatedOn { get; set; }
    public override Guid? OwnerGuid { get; set; }
    public override AccessRoleType RoleType { get; set; }
    public override string? Title { get; set; }


    public DataUserAccount()
    {
    }

    public DataUserAccount(IUserAccount user)
    {
        Guid = user.Guid;
        Login = user.Login;
        Blocked = user.Blocked;
        Cellular = user.Cellular;
        Email = user.Email;
        Title = user.Title;
        RoleType = user.Role.RoleType;
        CreatedOn = user.CreatedOn;
       
    }


}

