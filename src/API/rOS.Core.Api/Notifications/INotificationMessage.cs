using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Core.Api.Notifications;

public interface INotificationMessage  : IDocument
{
    INotificationAddress[] To { get; }

    string Subject { get; }
    string Body    { get; }

}
