using rOS.Security.Api.Accounts;

namespace rOS.Security.Api.Validators;

public interface IAccessRoleVaidator
{
    string RoleName { get; }
    IAccessRoleValidationResult Validate(ISecurityContext context);

}