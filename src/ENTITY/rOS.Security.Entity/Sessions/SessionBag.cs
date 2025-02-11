using rOS.Security.Api;
using rOS.Security.Api.Sessions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoboHub.Security.Entity
{
    public class SessionBag :  ISessionBag
    {

        private Dictionary<string, ISessionData> m_session_data = new Dictionary<string, ISessionData>();


        public ISessionData GetData(string name)
        {
        
            if (m_session_data.TryGetValue(name, out ISessionData? s))
            {
                return s;
            }

            return SessionData.Empty;
        }

        public ISessionData PutData(string name, object value)
        {
            ISessionData s;
            m_session_data[name] =s = new SessionData { Key = name, Value = value };
            return s; 
        }

        public IEnumerator<ISessionData> GetEnumerator() => m_session_data.Values.OfType<ISessionData>().GetEnumerator();
        

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        

        public SessionBag()
        { }

        public SessionBag(ISessionBag session) 
        {
            SessionBag? s = session as SessionBag;

            if (s != null)
            {
                m_session_data = s.m_session_data.Values.ToDictionary(x=>x.Key , x=>new SessionData {Key = x.Key , Value = x.Value } as ISessionData);
            }
        }

        public SessionBag(IDictionary<string, object> data)
        {
            m_session_data = data.ToDictionary(x => x.Key, x => new SessionData { Key = x.Key, Value = x.Value } as ISessionData);
        }
    }
}
