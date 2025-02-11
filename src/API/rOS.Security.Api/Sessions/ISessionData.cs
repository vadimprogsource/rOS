using System;
using System.Collections.Generic;
using System.Text;


namespace rOS.Security.Api.Sessions;


public interface ISessionData
{
    string Key   { get; }
    object Value { get; }
}


