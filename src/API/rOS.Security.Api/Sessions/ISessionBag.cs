using System;
using System.Collections.Generic;
using System.Text;


namespace rOS.Security.Api.Sessions;

public interface ISessionBag : IEnumerable<ISessionData>
{
   ISessionData GetData(string name);
   ISessionData PutData(string name,object value);
}
