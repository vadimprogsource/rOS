using System;
namespace rOS.Security.Api.Permissions;

public enum AccessPermission : byte {Deny= 0 , SelfOnly, FullAcess };



public interface IPermissionAction
{
    AccessPermission Create { get; }
    AccessPermission Read { get; }
    AccessPermission Update { get; }
    AccessPermission Delete { get; }

}

