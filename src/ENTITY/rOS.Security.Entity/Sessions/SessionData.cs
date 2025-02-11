using rOS.Security.Api;
using rOS.Security.Api.Sessions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoboHub.Security.Entity
{
    public class SessionData : ISessionData
    {

        public static readonly SessionData Empty = new SessionData();

        public string Key { get; set; } = string.Empty;

        public object Value { get; set; } = string.Empty;



    }
}
