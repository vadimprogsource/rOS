using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Core.Api.Personalize;

namespace rOS.Core.Api.Providers;

public interface  IPersonalizeProvider
{
    Task<IUserProfile> GetProfileAsync(Guid ownerGuid);

}
