using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Core.Api.Notifications;

public interface INotificationEvent : INamed
{
    int EventId { get; }
}
