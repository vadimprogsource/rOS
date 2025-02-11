namespace rOS.Security.Api.Validators;

public interface IAccessRoleValidationResult
{
    bool Success { get; }

    bool HasErrors { get; }

    IEnumerable<IAccessRoleValidationError> Errors { get; }   
}