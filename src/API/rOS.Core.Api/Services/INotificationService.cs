using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rOS.Core.Api.Notifications;

namespace rOS.Core.Api.Services;

public interface INotificationService
{
     Task<bool> SendNoficationAsync(INotificationEvent ev , INotificationMessage message);
}
