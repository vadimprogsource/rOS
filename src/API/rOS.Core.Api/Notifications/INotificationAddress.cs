using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Core.Api.Notifications;


public enum AddressType : byte {Phone , Email , Messenger  }

public interface INotificationAddress
{
    AddressType Type { get; }
    string Address { get; }
}
