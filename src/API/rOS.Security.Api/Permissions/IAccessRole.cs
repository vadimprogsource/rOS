using rOS.Core.Api;


namespace rOS.Security.Api.Permissions;

public enum AccessRoleType : int {Guest =0 , User = 1 , PowerUser = 10 , Admin = 100 , SuperVisor = byte.MaxValue };


public interface IAccessRole :IEntity, ITitled
{

    IAccessRestrictArea?  RestrictArea { get; }
    AccessRoleType RoleType     { get; }

    bool IsSupervisorRole { get; }
    bool IsAdminRole      { get; }
    bool IsPowerUserRole  { get; }
    bool IsUserRole       { get; }
    bool IsGuestRole      { get; }
    bool IsSystemRole { get; }


    IPermissionAction[] Permissions { get; }

    IAccessObject[] AccessObjects { get; }
}


public interface IAccessRestrictArea :IEntity , ITitled
{
     IAccessRole[] Roles { get; }
}
