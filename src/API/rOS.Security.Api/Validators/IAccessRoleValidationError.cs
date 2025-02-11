namespace rOS.Security.Api.Validators;

public interface IAccessRoleValidationError
{
    int Code { get; }
    string Reason { get; }
}