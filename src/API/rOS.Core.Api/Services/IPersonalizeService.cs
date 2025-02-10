
using System;
using System.Threading.Tasks;
using rOS.Core.Api.Orgs;
using rOS.Core.Api.Personalize;

namespace rOS.Core.Api.Services;

public interface IPersonalizeService
{
    Task<IUserProfile> CreateNewProfileAsync(Guid ownerGuid, IContact contact);
    Task<IUserProfile> SaveProfileAsync(Guid ownerGuid, IUserProfile profile);
    Task<IUserProfile> AddOrgToProfileAsync(Guid ownerGuid, IBusinessOrg org);
    Task<IUserProfile> RemoveOrgFromProfileAsync(Guid ownerGuid, IBusinessOrg org);
    Task<IUserProfile> AssignAddressToProfile(Guid ownerGuid, IAddress address);
    Task<IUserProfile> AssingPassportToProfile(Guid ownerGuid, IPassport passport);
    Task<IUserProfile> AssingContactToProfile(Guid ownerGuid, IContact contact);

}
