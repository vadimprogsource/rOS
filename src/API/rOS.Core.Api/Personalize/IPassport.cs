

namespace rOS.Core.Api.Personalize;

public interface IPassport
{

    string SurName { get; }
    string GivenNames { get; }
    string PassportNo       { get; }
    string IdentificationNo { get; }
    string Nationality { get; }
    DateTime BirthDate { get; }
    string PlaceOfBirth { get; } 
    DateTime IssueDate { get; }
    DateTime ExpireDate { get; }
    string Authority { get; }
}
