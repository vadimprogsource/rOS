using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace rOS.Security.Api.Storages;

public interface ISecurityStorage
{
    Task ForceClearExpired();
}
